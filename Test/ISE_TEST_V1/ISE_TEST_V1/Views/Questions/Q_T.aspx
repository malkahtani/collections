<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.ViewModels.Q_T_IndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Q_T
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Q_T</h2>
<form action="Questions/Q_T" method="post" id="C_T">
 
      <input type="hidden" id="qid" name="qid" value=" <%: Model.QID %>" />
      <input type="hidden" id="qtype" name="qtype" value=" <%: Model.QType %>" />
      
     
      <select id="tests" name="tests">
      <% for (int i =0 ; i<Model.TName.Count; i++)
         {
             %> 
        
    <option value = "<%:Model.TID[i] %>"><%: Model.TName[i]%></option>
    
    
 <% } %>
 </select>
  <input type ="submit" value="Submit" />
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
