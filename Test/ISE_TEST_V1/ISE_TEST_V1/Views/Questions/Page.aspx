<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.Models.PagedList<ISE_TEST_V1.Models.Question>>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Questions
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% ISE_TEST_V1.Models.HtmlHelpers.ShowPagerControl(this, Model, "Questions", "Page");
   
      %>
<p>
        <%: Html.ActionLink("Create New", "Test")%>
    </p>
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
                <%= System.Web.HttpUtility.HtmlDecode(item.Text)%>
            </td>
            <td>
                <%:  ISE_TEST_V1.Models.QType.Type[item.Type] %>
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

    <% ISE_TEST_V1.Models.HtmlHelpers.ShowPagerControl(this, Model, "Questions", "Page"); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
