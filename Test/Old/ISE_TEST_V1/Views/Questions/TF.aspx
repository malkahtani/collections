<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ISE_TEST_V1.ViewModels.TF_VM>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	TF
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>TF</h2>
   
     <% 
         int t_id = Model.ElementAt(0).TID;
        using (Html.BeginForm("TF_Add", "Questions",FormMethod.Post))
        {
            
           // Model.tf.q.type = Model.qt;
           // Model.tf.q.N_Answers = Model.NOA; %>
        <%: Html.ValidationSummary(true)%>
     <div class="display-label">Enter the text for your question</div>
     <%: Html.TextAreaFor(m => m[0].tf.q.Text, new { cols = 70, rows = 3 }) %> 
          <%//: Html.Telerik().EditorFor(m => m[0].tf.q.Text) %>
                 <div class="display-label">Enter the wight for your question</div>
     <%: Html.TextBoxFor(m => m[0].tf.q.Wight)%>
    
     
     
     <% int myloop = Model.ElementAt(0).NOA;
        for (int i = 0; i < myloop; i++)
        {
            if (i > 0)
            {
                QuestionsClass myQ = new QuestionsClass();
                myQ.Text = "";
                myQ.Wight = 0;
                myQ.type = 1;
                myQ.N_Answers = myloop;
                Model.ElementAt(i).tf.q = myQ;
            }
            
            Model.ElementAt(i).NOA = myloop;
            Model.ElementAt(i).qt = 1;
            Model.ElementAt(i).TID = t_id;
            Model.ElementAt(i).tf.my_tf_answers.A_Order = myloop+1;
            
            %>
      <div class="display-label">Enter the text for your answer</div>
        <%: Html.TextAreaFor(m => m[i].tf.my_tf_answers.Text)%>

        <%: Html.HiddenFor(m => m[i].NOA)%>
        <%: Html.HiddenFor(m => m[i].qt)%>
         <%: Html.HiddenFor(m => m[i].TID)%>
         <%: Html.HiddenFor(m => m[i].tf.my_tf_answers.A_Order)%>
        <div class="display-label">Is it the right answer</div>
        <%: Html.CheckBoxFor(m => m[i].tf.my_tf_answers.is_right)%>

        

       <% } %>
        <p>
                <input type="submit" value="Add Question" />
            </p>
     <%Html.EndForm();
        }%> 


   
    
   
</asp:Content>