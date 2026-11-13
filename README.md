# Municipal Services Portal (Parts 1, 2 & 3 Complete)

A C# .NET Windows Forms desktop application designed to streamline municipal service delivery in South Africa. Citizens can report local infrastructure issues, browse and receive recommendations for community events, and track service request statuses using self-balancing AVL trees, binary min-heaps, and weighted graphs.

## Features & Data Structures

### Module 1: Report Issues (Part 1)
- **Interactive Issue Logging:** Citizens log infrastructure faults with a location, locked category dropdown, detailed description, exact submission timestamp (`SubmittedDate`), and optional Windows `OpenFileDialog` file attachments.
- **Real-Time Progress Bar:** Dynamically changes colour from grey to orange to green as mandatory fields are completed.
- **Cross-Module Integration (`List<Issue>`):** Stores reported issues in memory and feeds them live into the Part 3 Service Request Status dashboard.

### Module 2: Local Events & Announcements (Part 2)
- **Chronological Event Storage (`SortedDictionary<DateTime, List<LocalEvent>>`):** Automatically keeps municipal events sorted by date while allowing multiple events on the same day.
- **Unique Category Dropdown (`HashSet<string>`):** Filters out duplicate categories so the search dropdown only shows unique options.
- **Search History (`Queue<string>`) & Recommendations:** Tracks the user's 5 most recent searches and suggests matching upcoming events.

### Module 3: Service Request Status (Part 3)
- **Self-Balancing AVL Tree (`ServiceRequestAvlTree`):** Organises service requests by Unique ID (`REQ-1001`, `REQ-1002`, etc.) using automatic left and right rotations for fast ID lookups and sorted In-Order display.
- **Priority Queue (`ServiceRequestMinHeap`) & Keyword Classifier:** Scans incoming citizen reports for urgent keywords (such as `"leakage"`, `"power outage"`, `"broken pipeline"`, `"burst"`, `"flood"`, or `"sinkhole"`) to assign urgency (`1 - Critical` to `4 - Low`) and uses a binary Min-Heap to bring critical emergencies to the top.
- **Smart Infrastructure Dependency Graph (`ServiceRequestGraph`):** Models related municipal faults as connected nodes with kilometre road distances, automatically linking new citizen reports to matching Pretoria departmental hubs.
- **Breadth-First Search (BFS) Traversal:** Finds and lists all connected faults in the same local infrastructure cluster.
- **Minimum Spanning Tree (Prim's Algorithm):** Calculates the shortest maintenance dispatch route to visit all connected faults without unnecessary loops.
- **Live Status Progression:** Allows users to select any request and advance its status from `Pending` -> `In Progress` -> `Resolved`.

## Prerequisites
- **IDE:** Visual Studio 2022 (with the .NET Desktop Development workload installed)
- **Framework:** .NET 8.0 SDK (or the Windows Forms .NET target configured in the solution)
- **OS:** Windows 10 or Windows 11

## How to Build and Run
1. Clone this repository to your computer:
   `git clone https://github.com/NathiMabena/MunicipalServicesApp.git`
2. Open **`MunicipalServicesApp.sln`** in **Visual Studio 2022**.
3. Ensure **`MunicipalServicesApp`** is set as the **Startup Project** in Solution Explorer.
4. Press **`Ctrl + Shift + B`** (or click **Build > Build Solution**) to compile the project.
5. Press **`F5`** (or click **Start**) to launch the application.

## User Guide
1. **Report Issues:** Click **Report Issues** on the Main Menu, fill in the Location, Category, and Description, and click **Submit Report**.
2. **Local Events and Announcements:** Click **Local Events and Announcements**, filter by Category or Date, and view personalised suggestions in the **Recommended For You** panel.
3. **Service Request Status:** Click **Service Request Status** to view all requests sorted by ID (`AVL Tree`) or urgency (`Min-Heap`), search by Unique ID (e.g., `REQ-1007` or `1007`), update ticket statuses, and click any row to inspect its **BFS Graph Traversal** and **Minimum Spanning Tree (MST)** dispatch route.