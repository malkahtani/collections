<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.Models.AddUserModel>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Add User
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create a New Account</h2>
    <p>
        Use the form below to create a new account. 
    </p>
    <p>
        Passwords are required to be a minimum of <%: ViewData["PasswordLength"] %> characters in length.
    </p>
    <%
        ISE_TEST_V1.Models.Roles r = new ISE_TEST_V1.Models.Roles();
        List<string> m_roles = new List<string>();
           string username = Page.User.Identity.Name;
        string[] roles = System.Web.Security.Roles.GetRolesForUser();
        for (int i = 0; i < roles.Count(); i++)
        {
            if( r.is_role_manager(roles[i],username))
            {
              m_roles.Add(roles[i]);  
            }
        }
        int n_of_r = m_roles.Count();
        
                         %>
    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.") %>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.UserName) %>
                </div>
                
                <div class="editor-field">
                    <%: Html.TextBoxFor(m => m.UserName) %>
                    <%: Html.ValidationMessageFor(m => m.UserName) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.Email) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(m => m.Email) %>
                    <%: Html.ValidationMessageFor(m => m.Email) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.Password) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.Password) %>
                    <%: Html.ValidationMessageFor(m => m.Password) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.ConfirmPassword) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.ConfirmPassword) %>
                    <%: Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </div>
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.Active_User) %>
                </div>
                <div class="editor-field">
                    <%: Html.CheckBoxFor(m => m.Active_User) %>
                    <%: Html.ValidationMessageFor(m => m.Active_User)%>
                </div>
                <% if (System.Web.Security.Roles.IsUserInRole("Admin"))
                   {
                       string[] a_roles = System.Web.Security.Roles.GetAllRoles();
                       %>
                       <%: Html.DropDownList("Role", new SelectList(a_roles))%>
                  <% }
                   else
                   {
                       if (n_of_r == 1)
                       { %>
                <%: Html.Hidden("Role", m_roles.ElementAt(0))%>
                <%}
                       else
                       {%>
                       <%: Html.DropDownList("Role", new SelectList(m_roles))%>
                 <%  }
                   }%>
                <p>
                    <input type="submit" value="Add User" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>