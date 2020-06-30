<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ISE_TEST_V1.Models.Question>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                Wight
            </th>
            <th>
                Text
            </th>
            <th>
                Type
            </th>
            <th>
                Number of Answers
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("Delete", "Delete", new { id=item.ID })%> |
                <%: Html.ActionLink("Link To Test", "Link_To_Test", new { id = item.ID })%> 
            </td>
            <td>
                <%: item.ID %>
            </td>
            <td>
                <%: item.Wight %>
            </td>
            <td>
                <%: item.Text %>
            </td>
            <td>
                <%: item.Type %>
            </td>
            <td>
                <%: item.N_Answers %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "Test")%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>

