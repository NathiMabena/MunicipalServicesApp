# Municipal Services Portal

A modern desktop application built in C# .NET Windows Forms to help citizens easily report local municipal issues and seamlessly engage with local events and announcements. 

## Features Highlight

- **Interactive Reporting:** Citizens can report issues with a location, category dropdown, and detailed description.
- **Local Events & Announcements:** Users can search and filter upcoming municipal events by specific categories or chronological dates.
- **Smart Recommendation Engine:** A proactive algorithm that analyzes user behavior patterns to predict and display highly personalized event suggestions based on search frequency.
- **Advanced Data Structures:** 
  - `List<T>`: Structurally manages and stores reported issues in memory.
  - `HashSet<T>`: Automatically filters out duplicate event categories to provide a clean, unique UI search dropdown.
  - `SortedDictionary<TKey, TValue>`: Automatically sorts events chronologically upon insertion for optimized retrieval.
  - `Queue<T>`: Acts as a rolling memory buffer to track the user's 5 most recent searches for the recommendation engine.
- **Gamified User Feedback:** A real-time dynamic progress bar tracks form completion, changing from orange to green to indicate when the user is cleared to submit.
- **Media Attachments:** Optional file attachment integration using native Windows dialogue components.
- **Defensive Programming:** Robust input validation across all forms to intercept empty queries and prevent unhandled exceptions gracefully.
- **Modern UI/UX:** Flat geometry design system featuring a cohesive SteelBlue theme, dynamic hover animations, and an intuitive visual hierarchy for inactive/pending features.

## Prerequisites

- Visual Studio 2022
- .NET 8.0 SDK (or the version targeted in your build)

## Compilation & Execution

1. Clone this repository to your local machine.
2. Open the `MunicipalServicesApp.sln` solution file in Visual Studio.
3. Ensure the startup project is set to `MunicipalServicesApp`.
4. Press **F5** or click the **Start** button at the top to build and run the application.

## How to Use

### Module 1: Report Issues
1. Launch the application to view the Main Menu.
2. Click **Report Issues** to open the reporting dashboard.
3. Fill in the Location, Category, and Description. Watch the progress bar dynamically update.
4. (Optional) Click **Media Attachment** to link a local file to your report.
5. Once the progress bar turns green (75% or 100%), click **Submit Report**.
6. **Assessor Note:** To verify the `List<T>` data structure is actively storing the `Issue` objects in memory, return to the Main Menu and click the **View Data (Admin)** button in the bottom left corner to view the active DataGridView.

### Module 2: Local Events and Announcements
1. From the Main Menu, click **Local Events and Announcements**.
2. Use the **Search By Category** dropdown (populated via `HashSet`) or the **Search By Date** picker to filter the events.
3. Click **Search** to view the filtered chronological results (handled via `SortedDictionary`).
4. Notice the **Recommended For You** section at the bottom left dynamically update based on your search history (powered by a `Queue` and a custom LINQ algorithm).
5. Click **Clear** to reset the data grid and empty the search filters.
