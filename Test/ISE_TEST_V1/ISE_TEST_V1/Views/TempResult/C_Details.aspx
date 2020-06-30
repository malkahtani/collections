<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.ViewModels.Temp_Reesult_Details>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	C_Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>The Appointment Result</h2>
    Candidate Details
    <fieldset>
        <legend>Candidate</legend>
        
        <div class="display-label">Candidate ID</div>
        <div class="display-field"><%: Model.c.ID %></div>
        
        <div class="display-label">Candidate Username</div>
        <div class="display-field"><%: Model.c.Uname%></div>
        
         
        <div class="display-label">Candidate Name</div>
        <div class="display-field"><%: Model.c.Name%></div>
        
        <div class="display-label">Candidate Mobile</div>
        <div class="display-field"><%: Model.c.Mobile%></div>
        
        <div class="display-label">Candidate Email</div>
        <div class="display-field"><%: Model.c.Email%></div>
        
    </fieldset>
    <%  ISE_TEST_V1.Models.Result ttr = null;
        int count = 0;
        for (int i =0 ; i<Model.t.Count; i++)
         {
           ISE_TEST_V1.Models.Test t =  Model.t.ElementAt(i);
           ISE_TEST_V1.Models.Temp_Result tr = Model.t_r.ElementAt(i);
           bool iden = Model.ind.ElementAt(i);
           if (iden)
           {
               ttr = Model.tt_r.ElementAt(count);
                count++;
           }
           
           %>
           <h2>The Test</h2>
           <table>
     <tr>
     <td>
      <div class="display-label">Test ID</div>
      </td>
      <td>
      <div class="display-label">Test Name</div>
      </td>
      <td>
      <div class="display-label">Test Description</div>
      </td>
      <td>
      <div class="display-label">Test Duration</div>
      </td>
      </tr>
      <tr>
     <td>
      <%: t.ID %>
      </td>
      <td>
      <%: t.Name %>
      </td>
      <td>
      <%: t.Description %>
      </td>
      <td>
      <%: t.Duration %>
      </td>
      </tr>
      </table>
      <% if((!iden) && ((tr.N_Q_N_M > 0) || (tr.N_Q_N_R>0))) {
              %>
              <h1><font color="red">This is not the final result there are questions have not been marked </font></h1>
              <h2>The Temporary Results for the above Test</h2>
      <table>
     <tr>
     <td>
      <div class="display-label">Total possible Marks</div>
      </td>
      <td>
      <div class="display-label">Total Marks for the Candidate</div>
      </td>
      <td>
      <div class="display-label">Number of Questions which need revison</div>
      </td>
      <td>
      <div class="display-label">Number of Questions which need to be marked</div>
      </td>
      <td>
      <div class="display-label">Number of Questions which auto marked</div>
      </td>
      <td>
      <div class="display-label">The Result %</div>
      </td>
      </tr>
      <tr>
      <td>
      <%: tr.Total_Marks %>
      </td>
      <td>
      <%: tr.User_Marks %>
      </td>
      <td>
      <%: tr.N_Q_N_R %>
      </td>
      <td>
      <%: tr.N_Q_N_M %>
      </td>
       <td>
      <%: tr.N_Q_M %>
      </td>
      <td>
      <p>%
      <%: tr.Result %></p>
      </td>
      </tr>
      </table>
       <%: Html.ActionLink("The Test Details", "Test",  new { id = tr.C_T_R_ID })%> 
              <%}
         else if (iden)
         {
              %>
              <h1><font color="green">This is the final result for this appointment </font></h1>
              <h2>The Results for the above Test</h2>
      <table>
     <tr>
     <td>
      <div class="display-label">Total possible Marks</div>
      </td>
      <td>
      <div class="display-label">Total Marks for the Candidate</div>
      </td>
      <td>
      <div class="display-label">Number of Questions which auto marked</div>
      </td>
     
      <td>
      <div class="display-label">Number of Questions which marked</div>
      </td>
      <td>
      <div class="display-label">The Result %</div>
      </td>
      </tr>
      <tr>
      <td>
      <%: ttr.Total_Marks%>
      </td>
      <td>
      
      <%: ttr.User_Marks %>
      </td>
      <td>
      <%: tr.N_Q_M%>
      </td>
      
       <td>
      <%: ttr.N_Q_M%>
      </td>
      <td>
      <p>%
      <%: ttr.Result1%></p>
      </td>
      </tr>
      </table>
      <%: Html.ActionLink("The Test Details", "Test",  new { id = tr.C_T_R_ID })%> 
              <%}
         else
         { %>
              <h1><font color="green">This is the final result for this appointment </font></h1>
              <h2>The Results for the above Test</h2>
              <table>
     <tr>
     <td>
      <div class="display-label">Total possible Marks</div>
      </td>
      <td>
      <div class="display-label">Total Marks for the Candidate</div>
      </td>
      <td>
      <div class="display-label">Number of Questions which need revison</div>
      </td>
      <td>
      <div class="display-label">Number of Questions which need to be marked</div>
      </td>
      <td>
      <div class="display-label">Number of Questions which auto marked</div>
      </td>
      <td>
      <div class="display-label">The Result %</div>
      </td>
      </tr>
      <tr>
      <td>
      <%: tr.Total_Marks %>
      </td>
      <td>
      <%: tr.User_Marks %>
      </td>
      <td>
      <%: tr.N_Q_N_R %>
      </td>
      <td>
      <%: tr.N_Q_N_M %>
      </td>
       <td>
      <%: tr.N_Q_M %>
      </td>
      <td>
      <p>%
      <%: tr.Result %></p>
      </td>
      </tr>
      </table>
         <%: Html.ActionLink("The Test Details", "Test",  new { id = tr.C_T_R_ID })%> 
              <%} %>
      
         <% } %>
</asp:Content>


