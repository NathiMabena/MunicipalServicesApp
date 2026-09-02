# Municipal Services Portal - Part 1

A modern desktop application built in C# .NET Windows Forms to help citizens easily report local municipal issues. 

## Features Highlight
* **Interactive Reporting:** Citizens can report issues with a location, category dropdown, and detailed description.
* **Gamified User Feedback:** A real-time dynamic progress bar tracks form completion, changing from orange to green to indicate when the user is cleared to submit.
* **Media Attachments:** Optional file attachment integration using native Windows dialogue components.
* **In-Memory Data Management:** Data is structurally managed using a custom `Issue` class and securely stored in a static `List<Issue>`.
* **Admin Verification:** Includes a hidden admin grid view to physically verify data structure bindings in real-time.
* **Modern UI/UX:** Flat geometry design system with dynamic colour states and "elevation card" contrast styling.

## Prerequisites
* Visual Studio 2022
* .NET 8.0 SDK (or the version targeted in your build)

## Compilation & Execution
1. Clone this repository to your local machine.
2. Open the `MunicipalServicesApp.sln` solution file in Visual Studio.
3. Ensure the startup project is set to `MunicipalServicesApp`.
4. Press **F5** or click the **Start** button at the top to build and run the application.

## How to Use
1. Launch the application to view the Main Menu.
2. Click **Report Issues** to open the reporting dashboard.
3. Fill in the Location, Category, and Description. Watch the progress bar dynamically update.
4. (Optional) Click **Media Attachment** to link a local file to your report.
5. Once the progress bar turns green (75% or 100%), click **Submit Report**.
6. **Assessor Note:** To verify the `List<T>` data structure is actively storing the `Issue` objects in memory, return to the Main Menu and click the **View Data (Admin)** button in the bottom left corner to view the active DataGridView.
