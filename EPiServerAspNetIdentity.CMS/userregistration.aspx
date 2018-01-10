<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="EPiServer.ServiceLocation" %>
<%@ Import Namespace="EPiServer.Shell.Security" %>

<%
    string adminRole = "WebAdmins";
    string username = "admin";
    string password = "P@ssw0rd";
    string email = "noreply@localhost.no";

    var userProvider = ServiceLocator.Current.GetInstance<UIUserProvider>();
    var roleProvider = ServiceLocator.Current.GetInstance<UIRoleProvider>();

    int userCount;
    userProvider.GetAllUsers(0, 1, out userCount);

    if (userCount > 0)
    {
        Response.Write("Database already contains users.");
        return;
    }

    UIUserCreateStatus status;
    var errors = Enumerable.Empty<string>();

    userProvider.CreateUser(username, password, email, string.Empty, string.Empty, true, out status, out errors);
    if (status == UIUserCreateStatus.Success)
    {
        roleProvider.CreateRole(adminRole);
        roleProvider.AddUserToRoles(username, new[] { adminRole });
        Response.Write("Created 'admin' user");
        return;
    }

    Response.Write("Failed to create user");
%>