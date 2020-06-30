<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.ViewModels.C_T_IndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Test
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Test</h2>
    <form action="/Candidate/C_T" method="post" id="C_T">
 
      <input type="hidden" id="cid" name="cid" value=" <%: Model.CID %>" />
     <table>
     <tr>
     <td>
      <select id="tests" name="tests">
      <% for (int i =0 ; i<Model.TName.Count; i++)
         {
             %> 
        
    <option value = "<%:Model.TID[i] %>"><%: Model.TName[i]%></option>
    
    
 <% } %>
 </select>
 </td>
 <td><p> Select the date that the candidate will sit the test</p>
     <%= Html.Telerik().DatePicker()
        .Name("Date")
%>
</td>
<td><p> Select the time that the candidate will sit the test</p>
<select id="time" name="time">
<%
    string[] mytime = new string[] { "8.00", "8.30","9.00","9.30","10.00","10.30","11,00","11.30","12.00","12.30","13.00","13.30","14.00","14.30","15.00","15.30","16.00","16.30" };
  for (int j = 0; j < mytime.Count(); j++)
  {
      %>
      <option value = "<%: j %>"><%: mytime[j]%></option>
 <% }
    %>


</select>
</td>
</tr>
</table>

      <input type ="submit" value="Submit" />
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
