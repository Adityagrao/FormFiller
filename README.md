# PDF Form Filler

A web application to manage users, generate PDF forms dynamically based on user input and download the filled PDFs. This project is designed to streamline the process of filling and submitting forms for multiple banks.

## Features
1. **Insert Users**: Add user details like name, date of birth, and address.
2. **View Users**: Display a list of all users in a table.
3. **Generate PDFs**: Select a form type and user ID to generate and download pre-filled PDFs.

## Technologies Used
- **HTML** and **CSS**: Frontend interface.
- **JavaScript**: Fetch API for backend communication.
- **Backend API**: RESTful API (Swagger documented).
- **CORS**: For cross-origin requests.

## How to run 
- Run the backend Solution in HTTPS mode - the Swagger needs to be running at https://localhost:7257/swagger/index.html if it is different, then please update in the HTML (const apiBaseUrl = "https://localhost:7257/api";)
- dotnet build
- dotnet run
- Run the index.html using VsCode Go Live


---

### How to Use

```markdown
## How to Use

### 1. Add a New User
- Fill out the form under the "Insert User" section.
- Click **Add User** to save the user.

### 2. View Users
- The "View Users" section will display all users in a table.

### 3. Generate PDFs
- Select a form type and a user ID in the "Select Form and User" section.
- Click **Download PDF** to generate and download the filled form.



