# Event-Mangament-CLDV6211
ventEase is a full-stack ASP.NET Core web application designed to streamline event planning, venue selection, and booking management. The application features a unique **"Dreamcore / Frutiger Aero"** aesthetic, blending the nostalgic, glossy high-tech feel of the mid-2000s with modern Glassmorphism UI trends.

## 🎨 Aesthetic Vision
The interface is inspired by the "Poolcore" aesthetic—characterized by clean white tiles, crystal clear azure water, and translucent "glass" surfaces. 
- **Glassmorphism:** UI elements utilize `backdrop-filter: blur()` to create a frosted glass effect over a high-fidelity pool background.
- **Retro-Modern:** Glossy 3D-style gradients (reminiscent of Windows Vista/7) paired with clean, minimalist layouts.
- **Consistent Branding:** A unified color palette of **Azure (#007FFF)** and **Pristine White**.

## 🚀 Features

### 🏢 Venue Management
- **Card-Based Explorer:** View available venues in a responsive grid of glossy glass cards.
- **Venue Details:** Track locations, guest capacities, and visual representations of event spaces.

### 📅 Event Scheduling
- **Visual Timeline:** Events are displayed in a modern calendar-list format.
- **Description & Tracking:** Comprehensive event details including descriptions and assigned venues.

### 📑 Booking System
- **Real-Time Overview:** A glass-paneled dashboard to manage all active bookings.
- **CRUD Operations:** Full Create, Read, Update, and Delete functionality for seamless coordination.

## 🛠️ Technical Stack
- **Framework:** ASP.NET Core MVC
- **Language:** C#
- **Database:** SQL Server (integrated with Entity Framework Core)
- **Frontend:** HTML5, CSS3 (Custom Glassmorphism), Bootstrap 4.0
- **Styling:** CSS3 Linear Gradients, Backdrop Filters, Flexbox/Grid Layouts

## 📂 Project Structure
- `Models/`: Contains entity definitions for `Venue`, `Event`, and `Booking`.
- `Views/`: Custom Razor views styled with the Azure/Poolcore theme.
- `wwwroot/images/`: Storage for the signature Dreamcore background and venue imagery.

## 🔧 Installation & Setup
1. **Clone the repository.**
2. **Database Setup:** - Ensure SQL Server is running.
   - Update the connection string in `appsettings.json`.
   - Run `Update-Database` via the Package Manager Console.
3. **Static Assets:** - Place your background image (e.g., `image_8af1ae.jpg`) in the `wwwroot/images/` folder.
4. **Run:** Press `F5` in Visual Studio to launch the application.

---
*Developed as part of an IT Computer and Application Development curriculum.*
