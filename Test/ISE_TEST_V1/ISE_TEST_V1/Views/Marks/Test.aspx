<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.ViewModels.T_Detaile>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Test Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>The Questions Which be given to the candidate and he's answers:</h2>
    <% int[] qtype1 = new int[3] { 1, 2, 3 };
       int[] qtype2 = new int[2] { 4,5 };
       int[] qtype3 = new int[2] { 7,8 };
       int qt = 6;
       int count = Model.Q.Count;
       for (int i = 0; i < count; i++)
       {
           int qtype =  Model.Q.ElementAt(i).Type;
           if(qtype1.Contains(qtype)){
                %> <br /><p>
  Question No. <%: i+1 %> </p><p> Question ID <%: Model.Q.ElementAt(i).ID%></p><p> <%= System.Web.HttpUtility.HtmlDecode(Model.Q.ElementAt(i).Text)%> 
</p><br /> <p>The Question Wight is <%: Model.Q.ElementAt(i).Wight%></p><br />

       <br /><p> The correct answers in the database for that question are:</p><br />
       <% for (int j = 0; j < Model.Answers.ElementAt(i).my_List.Count; j++)
          {
              ISE_TEST_V1.Models.Answer ans = Model.Answers.ElementAt(i).my_List.ElementAt(j);  %>
              <
               <% string text = System.Web.HttpUtility.HtmlEncode(ans.Text);
                  
               %>
                               
             
            <p>Answer No. <%: j + 1 %></p><%= System.Web.HttpUtility.HtmlDecode(ans.Text) %><br />
       <% } %>
<br />The user answer for that question is:<br />
<br /><%: Model.U_Answers.ElementAt(i).my_List.ElementAt(0).Text%> <br /><br />
           
           <%}else if(qtype2.Contains(qtype)){
           }else if(qtype == qt){
           }else if(qtype3.Contains(qtype)){
           
           }
            %>
            <%} %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
