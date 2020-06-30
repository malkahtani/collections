<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<ISE_TEST_V1.Models.Test>>" %>

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
                Test Name
            </th>
            <th>
                Description
            </th>
            <th>
                Duration in minutes
            </th>
            <th>
                Number of True and False Questions
            </th>
            <th>
               Number of multiple choice single answer Questions
            </th>
            <th>
                Number of multiple choice multiple answer Questions
            </th>
            <th>
                Number of Fill The Gap (String) Questions
            </th>
            <th>
               Number of Fill The Gap (Integer) Questions
            </th>
            <th>
                Number of Matching Table Questions
            </th>
            <th>
                Number of Short Answer Questions
            </th>
            <th>
                Number of Essay Questions
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id=item.ID }) %> |
                <%: Html.ActionLink("Details", "Details", new { id=item.ID })%> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.ID })%>
            </td>
            <td>
                <%: item.ID %>
            </td>
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: item.Description %>
            </td>
            <td>
                <%: item.Duration %>
            </td>
            <td>
                <%: item.N_Q_TF %>
            </td>
            <td>
                <%: item.N_Q_MCSA %>
            </td>
            <td>
                <%: item.N_Q_MCMA %>
            </td>
            <td>
                <%: item.N_Q_FGS %>
            </td>
            <td>
                <%: item.N_Q_FGI %>
            </td>
            <td>
                <%: item.N_Q_MT %>
            </td>
            <td>
                <%: item.N_Q_SA %>
            </td>
            <td>
                <%: item.N_Q_EA %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

