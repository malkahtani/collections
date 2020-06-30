<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ISE_TEST_V1.Models.Candidate>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	C_List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Candidates List</h2>

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
                Password
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
             |
                <%: Html.ActionLink("Check The Candidate Result", "C_Details", new { id=item.ID }) %> 
            </td>
            <td>
                <%: item.ID %>
            </td>
            <td>
                <%: item.Uname %>
            </td>
            <td>
                <%: item.Password %>
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

<asp:Content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>

