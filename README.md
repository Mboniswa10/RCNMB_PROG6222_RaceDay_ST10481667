# RCNMB_PROG6222_RaceDay_ST10481667
# RaceDay – Race Event Management System

## PROG6212 – Programming 2: Part 3 (Final POE)

**Project Name:** RaceDay  
**Application Type:** Web-Based Race Event Management System  
**Framework:** ASP.NET Core MVC and ASP.NET Core Web API  
**Target Framework:** .NET 8  
**Database:** Microsoft SQL Server (LocalDB)  
**IDE:** Microsoft Visual Studio 2022  

---

## 1. Project Overview

RaceDay is a web-based race event management system developed as part of the PROG6212 Programming 2 Practical Outcomes Evaluation (POE).

The purpose of the system is to simplify the management of race events by providing a central platform where organisers can manage events and participants can register, enrol in events and view their enrolment information.

The application uses ASP.NET Core Web API for backend operations and ASP.NET Core MVC for the user interface. Microsoft SQL Server is used to store application data.

The system supports two main user roles:

- **Organiser:** Can access the organiser dashboard, create and manage race events, and view event-related information.
- **Participant:** Can access the participant dashboard, browse available events, enrol in events and view their enrolments.

---

## 2. Project Objectives

The main objectives of the RaceDay system are to:

1. Develop a functional race event management application.
2. Implement a relational database using Microsoft SQL Server.
3. Develop RESTful API endpoints for application data.
4. Implement user registration and login functionality.
5. Secure authenticated endpoints using JSON Web Tokens (JWT).
6. Implement role-based access for organisers and participants.
7. Develop an MVC frontend that communicates with the Web API.
8. Allow organisers to create race events.
9. Allow participants to browse and enrol in available events.
10. Provide access to event, enrolment, category and results information.
11. Test the application and verify that its main features work correctly.

---

## 3. Technologies and Tools Used

The following technologies and tools were used during development:

| Technology | Purpose |
|---|---|
| C# | Main programming language |
| .NET 8 | Application development framework |
| ASP.NET Core Web API | Backend services and RESTful endpoints |
| ASP.NET Core MVC | Frontend web application |
| Entity Framework Core | Database access and object-relational mapping |
| Microsoft SQL Server | Relational database management |
| SQL Server LocalDB | Local database development |
| JWT | Authentication and API security |
| Swagger / OpenAPI | API documentation and testing |
| xUnit | Automated unit testing |
| Entity Framework InMemory | Database testing |
| Visual Studio 2022 | Development environment |
| Git and GitHub | Source control and project repository |
| HTML and Razor Views | MVC user interface |

---

## 4. Solution Structure

The RaceDay solution contains the following projects:

```text
RaceDay/
│
├── RaceDay.Api/
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── EventsController.cs
│   │   ├── CategoriesController.cs
│   │   ├── EnrolmentsController.cs
│   │   └── ResultsController.cs
│   │
│   ├── Models/
│   │   ├── AppUser.cs
│   │   ├── Event.cs
│   │   ├── Category.cs
│   │   ├── Enrolment.cs
│   │   └── Result.cs
│   │
│   ├── Data/
│   ├── appsettings.json
│   └── Program.cs
│
├── RaceDay.Contracts/
│
├── RaceDay.Api.Tests/
│   ├── ResultsControllerTests.cs
│   └── UnitTest1.cs
│
├── RaceDay.Mvc/
│   ├── Controllers/
│   │   ├── AccountController.cs
│   │   ├── ParticipantController.cs
│   │   └── OrganiserController.cs
│   │
│   ├── Models/
│   │   ├── LoginViewModel.cs
│   │   ├── RegisterViewModel.cs
│   │   ├── EventViewModel.cs
│   │   └── CreateEventViewModel.cs
│   │
│   ├── Services/
│   │   └── RaceDayApiService.cs
│   │
│   ├── Views/
│   │   ├── Account/
│   │   ├── Participant/
│   │   └── Organiser/
│   │
│   ├── appsettings.json
│   └── Program.cs
│
└── README.md
```

The structure separates the backend API, shared contracts, automated tests and MVC frontend into individual projects.

---

## 5. Database Design

The application uses Microsoft SQL Server to store and manage race event information.

The original database design includes the following main entities:

### Users
Stores user account information, including:
- User ID
- Full name
- Email address
- Password hash
- User role
- Account creation date

### Events
Stores race event information, including:
- Event ID
- Organiser ID
- Event name
- Description
- Event type
- Event date
- Start time
- Venue and location information
- Distance
- Maximum participants
- Entry fee
- Event status

### Categories
Stores race categories associated with events.

### EventCategories
Connects events to their available categories.

### Enrolments
Stores participant enrolments for race events.

### Results
Stores race result information associated with enrolments.

The database uses primary keys and foreign keys to maintain relationships between the entities.

**Database name:** RaceDay

**Development connection string:**

```text
Server=(localdb)\MSSQLLocalDB;
Database=RaceDay;
Trusted_Connection=True;
TrustServerCertificate=True;
```

The connection string is configured in the API application's `appsettings.json` file.

---

## 6. Backend API Development

The RaceDay API was developed using ASP.NET Core Web API and Entity Framework Core.

The API handles application data and provides endpoints that the MVC frontend can access.

The main controllers are:

### AuthController
Responsible for:
- User registration.
- User login.
- Password hashing and verification.
- Generating JWT authentication tokens.
- Returning authenticated user information.

### EventsController
Responsible for:
- Retrieving all race events.
- Retrieving an individual event.
- Creating race events.
- Restricting event creation to authorised organisers.

### CategoriesController
Responsible for retrieving race categories.

### EnrolmentsController
Responsible for retrieving participant enrolment information.

### ResultsController
Responsible for retrieving race results.

---

## 7. API Endpoints

The following endpoints were implemented and tested during development.

| HTTP Method | Endpoint | Description |
|---|---|---|
| POST | `/api/Auth/register` | Register a new user |
| POST | `/api/Auth/login` | Authenticate a user |
| GET | `/api/Events` | Retrieve all events |
| GET | `/api/Events/{id}` | Retrieve an individual event |
| POST | `/api/Events` | Create an event |
| GET | `/api/Categories` | Retrieve all categories |
| GET | `/api/Categories/{id}` | Retrieve an individual category |
| GET | `/api/Enrolments` | Retrieve enrolments |
| GET | `/api/Enrolments/{id}` | Retrieve an individual enrolment |
| GET | `/api/Results` | Retrieve results |
| GET | `/api/Results/{id}` | Retrieve an individual result |

Some endpoints require authentication and appropriate role permissions.

---

## 8. Authentication and Security

JWT authentication was implemented to protect the API.

The authentication process works as follows:

1. A user registers using the registration page.
2. The application stores the user's password as a secure password hash.
3. The user logs in using their email address and password.
4. The API verifies the supplied credentials.
5. The API generates a JWT token after successful authentication.
6. The MVC application stores the token in the user's session.
7. Protected API requests use the token for authentication.
8. Role-based authorisation controls access to organiser functionality.

The system distinguishes between Organiser and Participant roles.

Organiser-only event creation is protected using role-based authorisation.

**Security measures implemented:**
- Password hashing.
- JWT authentication.
- Token validation.
- Role-based authorisation.
- Session management in the MVC application.

JWT signing keys and database credentials should be stored securely and should not be committed as real production secrets to a public repository.

---

## 9. MVC Frontend Development

The RaceDay MVC application provides the user interface through which users interact with the system.

The MVC frontend communicates with the RaceDay API using the `RaceDayApiService`.

### 9.1 Account Management

The Account section includes:

- User registration.
- User login.
- Logout functionality.
- Session management.
- Redirecting users to dashboards based on their roles.

### 9.2 Participant Dashboard

The participant dashboard provides access to:

- Participant home page.
- Available race events.
- My Enrolments.
- Results.
- Profile information.

Participants can browse available events and enrol in events through the application.

### 9.3 Organiser Dashboard

The organiser dashboard provides access to:

- Organiser home page.
- Events.
- Create Event.
- Categories.
- Enrolments.
- Results.

Organisers can create race events and view event-related enrolment information.

---

## 10. Main Application Features

The following application workflows were implemented and manually tested:

### User Registration and Login
Users can register an account and log in using their credentials.

### Role-Based Dashboards
Users are redirected to the appropriate dashboard according to their assigned role.

### Event Management
Organisers can access the Create Event page, complete the event form and submit a new race event.

### Participant Event Browsing
Participants can view available race events.

### Participant Enrolment
Participants can enrol in an available event and view their enrolment under My Enrolments.

### Organiser Enrolment Management
Organisers can view participant enrolment information through the organiser dashboard.

### API Integration
The MVC frontend communicates with the backend API to retrieve and submit application data.

---

## 11. Running the Application

The following steps explain how to run the RaceDay application locally.

### Prerequisites

Ensure that the following software is installed:

- Microsoft Visual Studio 2022.
- .NET 8 SDK.
- SQL Server LocalDB.
- Microsoft SQL Server Management Studio (SSMS), if required for database inspection.

### Step 1: Open the Solution

1. Open Visual Studio 2022.
2. Open the RaceDay solution.
3. Wait for the projects and NuGet packages to load.

### Step 2: Check the Database

1. Open SQL Server Object Explorer or SSMS.
2. Connect to `(localdb)\MSSQLLocalDB`.
3. Locate the `RaceDay` database.
4. Confirm that the required tables and data are available.

Do not rerun a database script that drops and recreates the database unless you have backed up the existing data and specifically intend to rebuild it.

### Step 3: Configure the API

Check the connection string in:

`RaceDay.Api/appsettings.json`

Ensure that it points to the correct local RaceDay database.

### Step 4: Configure the MVC Application

Check the API base URL in:

`RaceDay.Mvc/appsettings.json`

Example:

```json
"ApiSettings": {
  "BaseUrl": "https://localhost:7184/"
}
```

The base URL must match the address at which the API is running.

### Step 5: Start the API

1. Set `RaceDay.Api` as the startup project.
2. Run the project.
3. Open the Swagger page to confirm that the API starts successfully.

Example:

`https://localhost:7184/swagger/index.html`

Keep the API running.

### Step 6: Start the MVC Application

1. Start `RaceDay.Mvc` in a second Visual Studio instance or run it while keeping the API process active.
2. Open the MVC application's local URL in a browser.
3. Register or log in using an appropriate account.
4. Navigate through the participant or organiser dashboard.

**Important:** The API must remain running while the MVC application communicates with it.

---

## 12. Testing and Verification

Testing was performed to verify the functionality of the backend API and the main application workflows.

### 12.1 Automated Testing

The `RaceDay.Api.Tests` project uses xUnit and Entity Framework Core InMemory for testing.

The reported automated test result was:

- Tests passed: 3
- Tests failed: 0

### 12.2 API Endpoint Testing

The following API responses were verified during development:

| Test | Expected Result | Observed Result |
|---|---|---|
| GET `/api/Events` | HTTP 200 | HTTP 200 |
| GET `/api/Events/1` | HTTP 200 | HTTP 200 |
| GET `/api/Categories` | HTTP 200 | HTTP 200 |
| GET `/api/Categories/1` | HTTP 200 | HTTP 200 |
| GET `/api/Enrolments` | HTTP 200 | HTTP 200 |
| GET `/api/Enrolments/1` | HTTP 200 | HTTP 200 |
| GET `/api/Enrolments/999999` | HTTP 404 | HTTP 404 |
| GET `/api/Results` | HTTP 200 | HTTP 200 |
| GET `/api/Results/1` | HTTP 200 | HTTP 200 |
| GET `/api/Results/999999` | HTTP 404 | HTTP 404 |

### 12.3 Manual Application Testing

The following workflows were also verified:

- Organiser registration/account access and login.
- Organiser dashboard access.
- Creating a race event.
- Viewing the created event.
- Participant registration.
- Participant login.
- Participant dashboard access.
- Browsing available events.
- Enrolling in an event.
- Viewing the enrolment under My Enrolments.
- Viewing participant enrolments from the organiser dashboard.

---

## 13. GitHub Repository

The project source code is maintained using Git and GitHub.

**GitHub Repository:**  
[PASTE YOUR GITHUB REPOSITORY LINK HERE]

The repository should contain the complete solution, including the API, MVC application, contracts, tests, SQL database scripts and README documentation.

The final repository should also contain the required meaningful commit history for the POE.

---

## 14. Demonstration Video

A demonstration video will be recorded and uploaded to YouTube to demonstrate the RaceDay application's main features and functionality.

The video should demonstrate:

1. Opening the RaceDay solution in Visual Studio.
2. Starting the API and displaying Swagger.
3. Registering or logging in to the application.
4. Logging in as an organiser.
5. Opening the organiser dashboard.
6. Creating a race event.
7. Viewing the created event.
8. Logging in as a participant.
9. Browsing available events.
10. Enrolling in an event.
11. Viewing the participant's enrolments.
12. Returning to the organiser dashboard to view enrolments.
13. Showing the successful build and automated test results.

### YouTube Demonstration Video Link

**YouTube URL:**

[PASTE YOUR YOUTUBE VIDEO LINK HERE]

**Video status:** To be completed after recording and uploading the demonstration video.

The video can be uploaded as an unlisted YouTube video if that is permitted by the lecturer's submission instructions. Ensure that the lecturer can access the link.

---

## 15. Additional Deployment and Submission Requirements

Before final submission, check the official POE instructions to confirm whether the following are required:

- Azure Blob Storage integration.
- Docker configuration.
- GitHub Actions or continuous integration.
- Cloud deployment.
- Additional screenshots or documentation.
- Required GitHub commit count.
- Demonstration video length and submission format.

These items should only be marked as completed after they have actually been implemented and verified.

---

## 16. Conclusion

The RaceDay project demonstrates the development of a web-based race event management system using C#, .NET 8, ASP.NET Core Web API, ASP.NET Core MVC and Microsoft SQL Server.

The application provides separate organiser and participant experiences, user registration and login, JWT authentication, event creation, event browsing and participant enrolment functionality.

The backend API and MVC frontend are connected to provide an integrated application. Automated API testing and manual application testing were performed to verify the main implemented features.

The project demonstrates the practical application of software development concepts, database integration, API development, authentication, role-based access, MVC architecture and software testing.

---

## 17. Final Submission Checklist

Before submitting the final POE, confirm each item below.

- [ ] Complete RaceDay solution is saved.
- [ ] `RaceDay.Api` builds successfully.
- [ ] `RaceDay.Mvc` builds successfully.
- [ ] `RaceDay.Contracts` is included.
- [ ] `RaceDay.Api.Tests` is included.
- [ ] Automated tests have been run.
- [ ] Database SQL scripts are included.
- [ ] Database structure and sample data are checked.
- [ ] Registration and login are demonstrated.
- [ ] Organiser dashboard is demonstrated.
- [ ] Participant dashboard is demonstrated.
- [ ] Event creation is demonstrated.
- [ ] Participant enrolment is demonstrated.
- [ ] Organiser enrolment viewing is demonstrated.
- [ ] Required screenshots are included.
- [ ] Required GitHub commits are checked.
- [ ] GitHub repository link is added above.
- [ ] Demonstration video is recorded.
- [ ] Demonstration video is uploaded to YouTube.
- [ ] YouTube video link is added in Section 14.
- [ ] README.md is saved in the root of the repository.
- [ ] Final submission follows the official POE instructions.


