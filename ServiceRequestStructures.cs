using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServicesApp
{
    // =========================================================================
    // AVL TREE (Self-Balancing Binary Search Tree for O(log n) ID Lookup)
    // =========================================================================
    public class AvlNode
    {
        public ServiceRequest Data { get; set; }
        public AvlNode Left { get; set; }
        public AvlNode Right { get; set; }
        public int Height { get; set; }

        public AvlNode(ServiceRequest data)
        {
            Data = data;
            Height = 1; // A new node starts at height 1
        }
    }

    public class ServiceRequestAvlTree
    {
        public AvlNode Root { get; private set; }

        private int GetHeight(AvlNode node) => node == null ? 0 : node.Height;

        // Calculates balance factor: Left Height minus Right Height
        private int GetBalance(AvlNode node) => node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);

        // Right Rotation: Fixes a Left-Heavy tree
        private AvlNode RotateRight(AvlNode y)
        {
            AvlNode x = y.Left;
            AvlNode T2 = x.Right;

            // Perform rotation
            x.Right = y;
            y.Left = T2;

            // Update heights
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x; // New root of this subtree
        }

        // Left Rotation: Fixes a Right-Heavy tree
        private AvlNode RotateLeft(AvlNode x)
        {
            AvlNode y = x.Right;
            AvlNode T2 = y.Left;

            // Perform rotation
            y.Left = x;
            x.Right = T2;

            // Update heights
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y; // New root of this subtree
        }

        public void Insert(ServiceRequest request)
        {
            Root = InsertRecursive(Root, request);
        }

        private AvlNode InsertRecursive(AvlNode node, ServiceRequest request)
        {
            // 1. Standard Binary Search Tree insertion
            if (node == null)
                return new AvlNode(request);

            if (request.NumericId < node.Data.NumericId)
                node.Left = InsertRecursive(node.Left, request);
            else if (request.NumericId > node.Data.NumericId)
                node.Right = InsertRecursive(node.Right, request);
            else
                return node; // Duplicate IDs are not allowed

            // 2. Update height of this ancestor node
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            // 3. Check balance factor to see if this node became unbalanced
            int balance = GetBalance(node);

            // Case 1: Left-Left Heavy
            if (balance > 1 && request.NumericId < node.Left.Data.NumericId)
                return RotateRight(node);

            // Case 2: Right-Right Heavy
            if (balance < -1 && request.NumericId > node.Right.Data.NumericId)
                return RotateLeft(node);

            // Case 3: Left-Right Heavy
            if (balance > 1 && request.NumericId > node.Left.Data.NumericId)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            // Case 4: Right-Left Heavy
            if (balance < -1 && request.NumericId < node.Right.Data.NumericId)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        // Fast O(log n) Binary Search by Unique Numeric ID
        public ServiceRequest Search(int numericId)
        {
            AvlNode current = Root;
            while (current != null)
            {
                if (numericId == current.Data.NumericId)
                    return current.Data; // Found it!
                else if (numericId < current.Data.NumericId)
                    current = current.Left;  // Target is smaller, go left
                else
                    current = current.Right; // Target is larger, go right
            }
            return null; // Not found
        }

        // In-Order Traversal (Left -> Root -> Right) returns requests sorted by ID
        public List<ServiceRequest> GetInOrderList()
        {
            List<ServiceRequest> result = new List<ServiceRequest>();
            InOrderRecursive(Root, result);
            return result;
        }

        private void InOrderRecursive(AvlNode node, List<ServiceRequest> result)
        {
            if (node == null) return;
            InOrderRecursive(node.Left, result);
            result.Add(node.Data);
            InOrderRecursive(node.Right, result);
        }
    }

    // =========================================================================
    // MIN-HEAP (Priority Queue for Instant Urgency / Priority Sorting)
    // =========================================================================
    public class ServiceRequestMinHeap
    {
        private List<ServiceRequest> heap = new List<ServiceRequest>();

        // Helper: Returns true if 'a' is more urgent than 'b'
        private bool IsHigherPriority(ServiceRequest a, ServiceRequest b)
        {
            if (a.Priority != b.Priority)
                return a.Priority < b.Priority; // 1 (Critical) comes before 2 (High)
            return a.NumericId < b.NumericId;   // Tie-breaker: older request ID first
        }

        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            int currentIndex = heap.Count - 1;

            // Bubble Up: Swap with parent while current node is higher priority
            while (currentIndex > 0)
            {
                int parentIndex = (currentIndex - 1) / 2;
                if (IsHigherPriority(heap[currentIndex], heap[parentIndex]))
                {
                    ServiceRequest temp = heap[currentIndex];
                    heap[currentIndex] = heap[parentIndex];
                    heap[parentIndex] = temp;
                    currentIndex = parentIndex;
                }
                else break;
            }
        }

        public ServiceRequest ExtractMin()
        {
            if (heap.Count == 0) return null;

            ServiceRequest min = heap[0];
            int lastIndex = heap.Count - 1;
            heap[0] = heap[lastIndex];
            heap.RemoveAt(lastIndex);

            // Bubble Down (Heapify): Restore heap order from the root down
            int currentIndex = 0;
            while (true)
            {
                int leftChild = 2 * currentIndex + 1;
                int rightChild = 2 * currentIndex + 2;
                int smallest = currentIndex;

                if (leftChild < heap.Count && IsHigherPriority(heap[leftChild], heap[smallest]))
                    smallest = leftChild;

                if (rightChild < heap.Count && IsHigherPriority(heap[rightChild], heap[smallest]))
                    smallest = rightChild;

                if (smallest != currentIndex)
                {
                    ServiceRequest temp = heap[currentIndex];
                    heap[currentIndex] = heap[smallest];
                    heap[smallest] = temp;
                    currentIndex = smallest;
                }
                else break;
            }

            return min;
        }

        // Extracts all requests in strict Priority Order without destroying the original heap
        public List<ServiceRequest> GetRequestsSortedByPriority()
        {
            ServiceRequestMinHeap tempHeap = new ServiceRequestMinHeap();
            foreach (var item in heap)
                tempHeap.Insert(item);

            List<ServiceRequest> sorted = new List<ServiceRequest>();
            while (tempHeap.heap.Count > 0)
                sorted.Add(tempHeap.ExtractMin());

            return sorted;
        }
    }

    // =========================================================================
    // GRAPH, BFS TRAVERSAL & MINIMUM SPANNING TREE (Prim's Algorithm)
    // =========================================================================
    public class GraphEdge
    {
        public int SourceId { get; set; }
        public int TargetId { get; set; }
        public double DistanceKm { get; set; }
        public string DependencyNote { get; set; }

        public GraphEdge(int sourceId, int targetId, double distanceKm, string dependencyNote)
        {
            SourceId = sourceId;
            TargetId = targetId;
            DistanceKm = distanceKm;
            DependencyNote = dependencyNote;
        }
    }

    public class ServiceRequestGraph
    {
        private Dictionary<int, List<GraphEdge>> adjacencyList = new Dictionary<int, List<GraphEdge>>();

        public void AddVertex(int requestId)
        {
            if (!adjacencyList.ContainsKey(requestId))
                adjacencyList[requestId] = new List<GraphEdge>();
        }

        public void AddEdge(int id1, int id2, double distanceKm, string dependencyNote)
        {
            AddVertex(id1);
            AddVertex(id2);

            adjacencyList[id1].Add(new GraphEdge(id1, id2, distanceKm, dependencyNote));
            adjacencyList[id2].Add(new GraphEdge(id2, id1, distanceKm, dependencyNote));
        }

        // Breadth-First Search (BFS): Finds all connected/dependent service requests in a cluster
        public List<int> BreadthFirstSearch(int startRequestId)
        {
            List<int> visitedOrder = new List<int>();
            if (!adjacencyList.ContainsKey(startRequestId)) return visitedOrder;

            HashSet<int> visited = new HashSet<int>();
            Queue<int> queue = new Queue<int>();

            visited.Add(startRequestId);
            queue.Enqueue(startRequestId);

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                visitedOrder.Add(current);

                foreach (GraphEdge edge in adjacencyList[current])
                {
                    if (!visited.Contains(edge.TargetId))
                    {
                        visited.Add(edge.TargetId);
                        queue.Enqueue(edge.TargetId);
                    }
                }
            }

            return visitedOrder;
        }

        // Prim's Algorithm: Generates the Minimum Spanning Tree (MST) for optimal maintenance dispatch
        public List<GraphEdge> CalculateMinimumSpanningTree(int startRequestId)
        {
            List<GraphEdge> mstEdges = new List<GraphEdge>();
            if (!adjacencyList.ContainsKey(startRequestId)) return mstEdges;

            HashSet<int> visited = new HashSet<int> { startRequestId };

            // Keep adding the shortest connecting edge until all reachable nodes in the cluster are visited
            while (true)
            {
                GraphEdge shortestEdge = null;

                foreach (int visitedNode in visited)
                {
                    foreach (GraphEdge edge in adjacencyList[visitedNode])
                    {
                        if (!visited.Contains(edge.TargetId))
                        {
                            if (shortestEdge == null || edge.DistanceKm < shortestEdge.DistanceKm)
                                shortestEdge = edge;
                        }
                    }
                }

                if (shortestEdge == null)
                    break; // All connected nodes have been reached

                visited.Add(shortestEdge.TargetId);
                mstEdges.Add(shortestEdge);
            }

            return mstEdges;
        }
    }
}