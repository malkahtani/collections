<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.ViewModels.QuestionsIndex>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Question to a Test</h2>
    <form action="Q" method="post" id="Q">
    <div class="display-label">Select the test that you want to add question to</div>
    <select id="tests" name="tests">
      <% for (int i =0 ; i<Model.TName.Count; i++)
         {
            %> 
        
    <option value = "<%:Model.TID[i] %>"><%: Model.TName[i]%></option>
    
    
 <% } %>
 </select>
 <div class="display-label">Select the question type</div>
 <select id="QType" name="QType">
      <%  string[] Q = new string[] { "True/False", "Multiple choice single answer", "multiple choice multiple answer", "Fill The Gap (String)", "Fill The Gap (Integer)", "Matching Table", "Short Answer", "Essay" };
          for (int j = 0; j < Q.Count(); j++)
  {
      %>
      <option value = "<%: j+1 %>"><%: Q[j]%></option>
 <% }
    %>
 </select>
 <div class="display-label">Select the number of possible answers to your question</div>
 <select id="NoA" name="NoA">
  <% for (int m = 1; m <= 10; m++)
  {
      %>
      <option value = "<%: m %>"> <%: m %></option>
 <% }
    %>
 </select>
 <br />
 <input type ="submit" value="Next" />
 </form>

</asp:Content>