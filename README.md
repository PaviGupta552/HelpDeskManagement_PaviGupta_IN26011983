<<<<<<< HEAD
# HelpDeskManagement
=======
# HelpDeskManagement_PaviGupta_IN26011983
>>>>>>> 3b9b3ad2db32c5553001591134b324822cbb50e5

A professional Help Desk Ticket Management System built using **ASP.NET Core Web API, ASP.NET Core MVC, Entity Framework Core, SQL Server, Repository Pattern, xUnit and Moq**.

The application allows Help Desk teams to create, view, update, filter and delete support tickets through a clean and user-friendly web interface.

## Features

### Ticket Management

* Create new support tickets
* View all tickets
* View complete ticket details
* Edit existing tickets
* Delete tickets
* Filter tickets by status
* Track ticket priority and status
* Record who raised each ticket
* Track ticket creation date

###  Dashboard

The application provides a dashboard displaying:

* Total Tickets
* Open Tickets
* Closed Tickets
* Complete ticket listing
* Quick actions for viewing, editing and deleting tickets

### User Interface

The MVC application includes a customized Help Desk interface with:

* Personalized Help Desk branding
* Custom dashboard
* Ticket statistics
* Status indicators
* Priority indicators
* Responsive ticket forms
* Custom Create, Edit, Details and Delete pages
* Consistent visual styling throughout the application


##  Technologies Used

| Technology            | Purpose                       |
| --------------------- | ----------------------------- |
| ASP.NET Core Web API  | Backend REST API              |
| ASP.NET Core MVC      | Web application / UI          |
| Entity Framework Core | Database access               |
| SQL Server Express    | Database                      |
| Repository Pattern    | Data access abstraction       |
| HttpClient            | MVC → API communication       |
| xUnit                 | Unit testing                  |
| Moq                   | Repository mocking            |
| Swagger               | API testing and documentation |
| Git & GitHub          | Version control               |

The assignment requires the MVC application to communicate with the Web API through a Service Layer using `HttpClient`, rather than accessing the database directly. 

#  Solution Architecture

HelpDeskManagement
│
├── HelpDesk.Api
│   ├── Controllers
│   │   └── TicketController.cs
│   │
│   ├── Data
│   │   └── HelpDeskDbContext.cs
│   │
│   ├── Models
│   │   └── Ticket.cs
│   │
│   └── Repositories
│       ├── ITicketRepository.cs
│       └── TicketRepository.cs
│
├── HelpDesk.Mvc
│   ├── Controllers
│   │   └── TicketController.cs
│   │
│   ├── Models
│   │   └── Ticket.cs
│   │
│   ├── Services
│   │   └── TicketService.cs
│   │
│   ├── Views
│   │   └── Ticket
│   │       ├── Index.cshtml
│   │       ├── Create.cshtml
│   │       ├── Edit.cshtml
│   │       ├── Details.cshtml
│   │       └── Delete.cshtml
│   │
│   └── wwwroot
│       └── css
│           └── site.css
│
├── HelpDesk.Tests
│   └── UnitTest1.cs
│
├── HelpDeskManagement.sln
├── README.md
└── .gitignore


#  API Endpoints

The Web API exposes the following ticket endpoints:

| Method | Endpoint                      | Description              |
| ------ | ----------------------------- | ------------------------ |
| GET    | `/api/Ticket/All`             | Get all tickets          |
| GET    | `/api/Ticket/{id}`            | Get ticket by ID         |
| POST   | `/api/Ticket`                 | Create a ticket          |
| PUT    | `/api/Ticket/{id}`            | Update a ticket          |
| DELETE | `/api/Ticket/{id}`            | Delete a ticket          |
| GET    | `/api/Ticket/Status/{status}` | Filter tickets by status |


#  Database

The application uses:

**SQL Server Express**

with:

Database: HelpDeskDb
Table: dbo.Tickets


Entity Framework Core is used for database operations through `HelpDeskDbContext`.

The application follows the **Repository Pattern**, with database operations handled through:


ITicketRepository
        ↓
TicketRepository
        ↓
HelpDeskDbContext
        ↓
SQL Server

#  Application Flow

The application follows this architecture:


User
 │
 ▼
HelpDesk.Mvc
 │
 ▼
TicketController
 │
 ▼
TicketService
 │
 │ HttpClient
 ▼
HelpDesk.Api
 │
 ▼
TicketController
 │
 ▼
ITicketRepository
 │
 ▼
TicketRepository
 │
 ▼
Entity Framework Core
 │
 ▼
SQL Server


#  Unit Testing

Unit tests are implemented using:

* **xUnit**
* **Moq**

The repository layer is mocked so that the tests do **not** connect to SQL Server.

Mandatory test scenarios include:

* Get all tickets when tickets exist
* Get ticket by ID when the ticket exists
* Get ticket by ID when the ticket does not exist
* Create ticket successfully
* Handle invalid/null ticket creation
* Get tickets by status







