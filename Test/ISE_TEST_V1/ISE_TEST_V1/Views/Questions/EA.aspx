<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ISE_TEST_V1.ViewModels.EA_VM>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EA
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EA</h2>
 <% 
         int t_id = Model.ElementAt(0).TID;
        using (Html.BeginForm("EA_Add", "Questions",FormMethod.Post))
        {
            
           // Model.tf.q.type = Model.qt;
           // Model.tf.q.N_Answers = Model.NOA; %>
        <%: Html.ValidationSummary(true)%>
     <div class="display-label">Enter the text for your question</div>
    
     <%= Html.Telerik().EditorFor(m => m[0].ea.q.Text).HtmlAttributes(new { style = "width: 400px;" }) %>
     <br />
     <div class="display-label">Enter the wight for your question</div>
     <%: Html.TextBoxFor(m => m[0].ea.q.Wight)%>
    
     <br />
     
     <% int myloop = Model.ElementAt(0).NOA;
        for (int i = 0; i < myloop; i++)
        {
            if (i > 0)
            {
                QuestionsClass myQ = new QuestionsClass();
                myQ.Text = "";
                myQ.Wight = 0;
                myQ.type = 8;
                myQ.N_Answers = myloop;
                Model.ElementAt(i).ea.q = myQ;
            }
            
            Model.ElementAt(i).NOA = myloop;
            Model.ElementAt(i).qt = 8;
            Model.ElementAt(i).TID = t_id;
            
            
            %>
      <div class="display-label">Enter the text for your answer</div>
      <div><%= Html.Telerik().EditorFor(m => m[i].ea.my_ea_answers.Text).HtmlAttributes(new { style = "width: 750px; high: 500px;" })%></div>
        <br />

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