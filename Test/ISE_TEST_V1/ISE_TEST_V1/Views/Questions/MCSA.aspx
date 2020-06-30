<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ISE_TEST_V1.ViewModels.MCSA_VM>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MCSA
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MCSA</h2>
    <% 
         int t_id = Model.ElementAt(0).TID;
        using (Html.BeginForm("MCSA_Add", "Questions",FormMethod.Post))
        {
            
           // Model.tf.q.type = Model.qt;
           // Model.tf.q.N_Answers = Model.NOA; %>
        <%: Html.ValidationSummary(true)%>
     <div class="display-label">Enter the text for your question</div>
     <%= Html.Telerik().EditorFor(m => m[0].mcsa.q.Text).HtmlAttributes(new { style = "width: 400px;" }) %>
     <div class="display-label">Enter the wight for your question</div>
     <%: Html.TextBoxFor(m => m[0].mcsa.q.Wight)%>
    
     
     
     <% int myloop = Model.ElementAt(0).NOA;
        for (int i = 0; i < myloop; i++)
        {
            if (i > 0)
            {
                QuestionsClass myQ = new QuestionsClass();
                myQ.Text = "";
                myQ.Wight = 0;
                myQ.type = 2;
                myQ.N_Answers = myloop;
                Model.ElementAt(i).mcsa.q = myQ;
            }
            
            Model.ElementAt(i).NOA = myloop;
            Model.ElementAt(i).qt = 2;
            Model.ElementAt(i).TID = t_id;
            Model.ElementAt(i).mcsa.my_mcsa_answers.A_Order = myloop+1;
            
            %>
      <div class="display-label">Enter the text for your answer</div>
        <%= Html.Telerik().EditorFor(m => m[i].mcsa.my_mcsa_answers.Text).HtmlAttributes(new { style = "width: 400px;" })%>
        <%: Html.HiddenFor(m => m[i].NOA)%>
        <%: Html.HiddenFor(m => m[i].qt)%>
         <%: Html.HiddenFor(m => m[i].TID)%>
         <%: Html.HiddenFor(m => m[i].mcsa.my_mcsa_answers.A_Order)%>
        <div class="display-label">Is it the right answer</div>
        <%: Html.CheckBoxFor(m => m[i].mcsa.my_mcsa_answers.is_right)%>

        

       <% } %>
        <p>
                <input type="submit" value="Add Question" />
            </p>
     <%Html.EndForm();
        }%> 

</asp:Content>
