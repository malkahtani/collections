<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ISE_TEST_V1.Models.Candidate>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
      <form action="Candidate/Search" method="post" id="searchForm">
      <table><tr><td><input type="text" name="searchTerm" id="searchTerm" value="" size="10" maxlength ="30" /></td>
      <td>
      <select id="by" name="by">
      <option value = "0">Search by ID</option>
      <option value = "1">Search by Username</option>
      <option value = "2">Search by Name</option>
      <option value = "3">Search by Mobile</option>
      <option value = "4">Search by email</option>
      </select>
      </td>
      </tr>
      </table>
      <input type ="submit" value="Search" />
    </form>

    <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Username
            </th>
            
            <th>
                Name
            </th>
            <th>
                Mobile
            </th>
            <th>
                Email
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id=item.ID }) %> |
                <%: Html.ActionLink("Details", "Details", new { id=item.ID })%> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.ID })%> |
                 <%: Html.ActionLink("Link To Test", "Test", new { id=item.ID }) %> 
            </td>
            <td>
                <%: item.ID %>
            </td>
            <td>
                <%: item.Uname %>
            </td>
           
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: item.Mobile %>
            </td>
            <td>
                <%: item.Email %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

