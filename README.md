# Email Sender API

## Overview

The Email Sender API is a simple and user-friendly tool designed to simplify email communication in your applications. It provides two APIs, with the potential for future expansion, to meet your email-sending needs. Whether you want to send regular emails or implement a password reset mechanism, this project has you covered.

### Features

**1. Regular Email Sender API**

This API allows you to send regular emails effortlessly. It accepts parameters such as subject, content, and more to tailor your emails exactly how you want them. You can easily integrate this API into your application, enhancing your user engagement and communication.

**2. Password Reset Email Sender API - Template**

Implementing a user-friendly password reset process is crucial for any application. This API simplifies the process by sending a new password to the specified email address, helping your users regain access to their accounts quickly and securely.

### Configuration

To get started with the Email Sender API, you need to configure the `appsettings.json` file to match your specific requirements. Modify the following settings under the "EmailSenderOptions" section:

- **MailServer**: Set the SMTP server address. By default, it is configured for Gmail as `smtp.gmail.com`.

- **MailPort**: Specify the SMTP server port. Gmail typically uses port `587`.

- **SenderName**: Enter the name of the sender, which will appear in the recipient's email.

- **SenderEmail**: Provide the email address from which the emails will be sent.

- **AppPassword**: To securely send emails via Gmail, you'll need an "App Password." You can generate one from following [App passwords]( https://myaccount.google.com/apppasswords). Make sure to select "Other (Custom name)" as the app and name it accordingly.

- **AllowedContentType**: Define the content type for your emails. By default, it is set to "text/html."

### Usage

Once you've configured your `appsettings.json` file with the appropriate settings, you can seamlessly incorporate the Email Sender API into your application. Use the provided APIs to send emails.
