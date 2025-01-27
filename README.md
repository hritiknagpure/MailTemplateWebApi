### **Project Overview:**

This project is an **ASP.NET Core Web API** designed to send emails through an SMTP server. It utilizes the **MailKit** library to handle email sending, attachment management, and custom email templates. The application follows a **Microservices architecture** and is set up to manage email notifications for users in a larger enterprise system.

### **Key Features:**
1. **Email Sending Service:**
   - The project includes a `MailService` class that sends emails asynchronously to a specified recipient.
   - The service is configured with SMTP settings (host, port, and credentials) to connect and authenticate with the email server.

2. **Email Template Support:**
   - The email body can be customized with dynamic content using an HTML email template.
   - Includes user-specific data like name, message, and dynamic links (such as social media links or website URLs).

3. **File Attachments:**
   - Supports adding attachments to the email, allowing users to send files alongside the email message.

4. **Configuration via `appsettings.json`:**
   - SMTP settings (like username, password, host, and port) are configured in the `appsettings.json` file for easy updates and deployment.

5. **API Endpoint for Sending Emails:**
   - The Web API exposes a `POST` endpoint (`/api/email/send`) where users can send an email by providing the necessary details like recipient email, subject, message body, and attachments.

6. **Security Considerations:**
   - Uses **SecureSocketOptions.StartTls** to ensure the email transmission is secure.
   - Sensitive data (SMTP password) is stored in configuration files and can be replaced with more secure options like environment variables for production deployments.

### **How it Works:**
- **Step 1**: A user calls the `/api/email/send` endpoint with the email details.
- **Step 2**: The email service constructs the email, attaching any provided files and setting the email body using an HTML template.
- **Step 3**: The service connects to the SMTP server and sends the email asynchronously.
- **Step 4**: The response is sent back to the user indicating whether the email was successfully sent.

### **Technologies Used:**
- **ASP.NET Core Web API**
- **MailKit** (for handling email sending and attachments)
- **HTML/CSS** (for email templates)
- **Swagger UI** (for API documentation)

### **Applications:**
- **Business Notifications**: Sending notifications or promotional emails to customers.
- **User Onboarding**: Sending welcome emails with user-specific data and links to resources.
- **Document Sharing**: Sending emails with attachments, such as reports or invoices.

This project can be extended to handle different types of emails, such as transactional emails, notifications, or marketing campaigns, as part of a larger business application.
