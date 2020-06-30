﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ISE_TEST_V1.ViewModels.SA_VM>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	FGS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>FGS</h2>
 <% 
         int t_id = Model.ElementAt(0).TID;
        using (Html.BeginForm("SA_Add", "Questions",FormMethod.Post))
        {
            
           // Model.tf.q.type = Model.qt;
           // Model.tf.q.N_Answers = Model.NOA; %>
        <%: Html.ValidationSummary(true)%>
     <div class="display-label">Enter the text for your question</div>
     <%: Html.TextAreaFor(m => m[0].sa.q.Text, new { cols = 70, rows = 3 })%>
     <div class="display-label">Enter the wight for your question</div>
     <%: Html.TextBoxFor(m => m[0].sa.q.Wight)%>
    
     
     
     <% int myloop = Model.ElementAt(0).NOA;
        for (int i = 0; i < myloop; i++)
        {
            if (i > 0)
            {
                QuestionsClass myQ = new QuestionsClass();
                myQ.Text = "";
                myQ.Wight = 0;
                myQ.type = 7;
                myQ.N_Answers = myloop;
                Model.ElementAt(i).sa.q = myQ;
            }
            
            Model.ElementAt(i).NOA = myloop;
            Model.ElementAt(i).qt = 7;
            Model.ElementAt(i).TID = t_id;
            
            
            %>
      <div class="display-label">Enter the text for your answer</div>
        <%: Html.TextAreaFor(m => m[i].sa.my_sa_answers.Text, new { cols = 70, rows = 3 })%>

        <%: Html.HiddenFor(m => m[i].NOA)%>
        <%: Html.HiddenFor(m => m[i].qt)%>
         <%: Html.HiddenFor(m => m[i].TID)%>
        

        

       <% } %>
        <p>
                <input type="submit" value="Add Question" />
            </p>
     <%Html.EndForm();
        }%> 

</asp:Content>