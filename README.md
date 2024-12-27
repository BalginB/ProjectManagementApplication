# Project Management Application (WPF)

A **WPF-based Project Management Application** designed to streamline task management using a **Kanban-style board**. The application allows users to track the progress of tasks through various stages like "To Do," "In Progress," "Code Review," "QA," and "Done". It also features a modern login interface with support for third-party integrations.

---

## Features

### Login System
- **Secure Login**: Users can log in using their email and password.
- **Third-party Integration**: Buttons for Google, GitHub, and LinkedIn for quick redirection to respective accounts.
- **Stylish Design**: Modern UI with gradient themes and clean layouts.

### Task Management (Main Window)
- **Add Task**: Create tasks in the "To Do" column with a single click.
- **Drag & Drop**: Move tasks across columns to update their status dynamically.
- **Task Deletion**: Right-click on a task to delete it.
- **Task Description Editing**: Click into a task card to update its details.
- **Real-time Updates**: Tasks are saved to a local SQLite database for persistence.
![image_2024-12-27_17-49-51](https://github.com/user-attachments/assets/b3af74ca-d73f-4ecc-a0ce-c583021985aa)

### User Interface
- **Gradient Design**: A visually appealing gradient background and card styling.
- **Responsive Design**: Smooth interactions for task creation and movement.
- **Search Bar**: Placeholder for searching tasks (UI available but not fully implemented).

---

## Screenshots

### Task Board (Main Window)
![image_2024-12-27_17-22-48](https://github.com/user-attachments/assets/deacf928-bc84-4173-b239-2ef97c7a0c03)

The task board is divided into five stages:
- **To Do**: For newly created tasks.
- **In Progress**: Tasks currently being worked on.
- **Code Review**: Tasks under review by peers.
- **QA**: Tasks under quality testing.
- **Done**: Completed tasks.

### Login Page
![image_2024-12-27_17-23-32](https://github.com/user-attachments/assets/7183624f-d988-4c5c-8f58-eaa08decb0ab)

The login page features:
- An option to sign in using an email and password.
- Buttons for Google, GitHub, and LinkedIn integration.
- A clean "Sign Up" section for potential future user registration.

---

## Technologies

- **WPF (Windows Presentation Foundation)** for building the UI.
- **SQLite** for local data storage using Entity Framework Core.
- **C#** as the core programming language.
- **Modern UI Design** with gradient colors and responsive components.

---

