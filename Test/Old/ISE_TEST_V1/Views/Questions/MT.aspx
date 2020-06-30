<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ISE_TEST_V1.ViewModels.MT_VM>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	MT
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>MT</h2>
 <% 
         int t_id = Model.ElementAt(0).TID;
        using (Html.BeginForm("MT_Add", "Questions",FormMethod.Post))
        {
            
           // Model.tf.q.type = Model.qt;
           // Model.tf.q.N_Answers = Model.NOA; %>
        <%: Html.ValidationSummary(true)%>
     <div class="display-label">Enter the text for your question</div>
     <%: Html.TextAreaFor(m => m[0].mt.q.Text, new { cols = 70, rows = 3 })%>
     <div class="display-label">Enter the wight for your question</div>
     <%: Html.TextBoxFor(m => m[0].mt.q.Wight)%>
    
      <table>
            <tr>
            <td><div class="display-label">No.</div></td>
            <td><div class="display-label">Enter the text for your statment</div></td>
            <td><div class="display-label">Enter Matching number from the left coulmun</div></td>
            <td><div class="display-label">Enter the text for your other statment</div></td>
            </tr>
     <% int myloop = Model.ElementAt(0).NOA;
        int negloop = (myloop*2)-1;
        for (int i = 0; i < myloop; i++)
        {
            if (i == 0)
            {
                int j = 0;
                if (j == 0)
                {
                    Model.ElementAt(j).NOA = myloop;
                    Model.ElementAt(j).qt = 6;
                    Model.ElementAt(j).TID = t_id;
                }

                for (j = 1; j < (myloop * 2) - 1; j++)
                {
                    QuestionsClass myQ = new QuestionsClass();
                    myQ.Text = "";
                    myQ.Wight = 0;
                    myQ.type = 6;
                    myQ.N_Answers = myloop;
                    Model.ElementAt(j).mt.q = myQ;
                    Model.ElementAt(j).NOA = myloop;
                    Model.ElementAt(j).qt = 6;
                    Model.ElementAt(j).TID = t_id;
                    
                    
                }
            }

            Model.ElementAt(i).mt.my_mt_answers.A_Order = i+1;
            Model.ElementAt(i).mt.my_mt_answers.PID = 0;
            Model.ElementAt(negloop-i).mt.my_mt_answers.A_Order = i+1;
            
            
            %>
            <tr>
            <td><p>  <%: i+1 %> -</p></td>
            <td><%: Html.TextAreaFor(m => m[i].mt.my_mt_answers.TEXT, new { cols = 50, rows = 7 })%></td>
            <td><%: Html.TextBoxFor(m => m[negloop-i].mt.my_mt_answers.PID)%></td>
            <td><%: Html.TextAreaFor(m => m[negloop-i].mt.my_mt_answers.TEXT, new { cols = 50, rows = 7 })%></td>
            </tr>
            
      
        
        <%: Html.HiddenFor(m => m[i].mt.my_mt_answers.A_Order)%>
         <%: Html.HiddenFor(m => m[negloop - i].mt.my_mt_answers.A_Order)%>
         <%: Html.HiddenFor(m => m[i].mt.my_mt_answers.PID)%>
        <%: Html.HiddenFor(m => m[i].NOA)%>
        <%: Html.HiddenFor(m => m[i].qt)%>
         <%: Html.HiddenFor(m => m[i].TID)%>
        

        

       <% } %>
        <p>
         </table>
                <input type="submit" value="Add Question" />
            </p>
     <%Html.EndForm();
            
        }%> 
       
</asp:Content>