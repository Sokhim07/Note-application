Helle,
This project cannot be hosted directly on GitHub Pages because it requires both the API and the UI to run locally.
Installation Steps:

- Clone or download the project.

- Restore the NOTEDB database file in your SQL Server.

- Navigate to the application_noteapi folder → open appsettings.json and update the connection string → open the terminal and run: dotnet watch

- Navigate to the application_noteui folder → open the terminal and run: npm run dev, Then open the localhost link in your browser.

- You can find the script in the SCRIPT_NOTE file.
