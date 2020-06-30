<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ISE_TEST_V1.ViewModels.MarksView>>"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Mark
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Mark</h2>
    <% using (Html.BeginForm("Go_Marks", "Marks", FormMethod.Post))
          {

              %>
        <%: Html.ValidationSummary(true)%>
    <% 
        
        int loop = Model.Count();
        for (int i = 0; i < loop; i++)
        {
            ISE_TEST_V1.Models.Question my_q = Model.ElementAt(i).Q;
            ISE_TEST_V1.Models.Answers_Log my_al = Model.ElementAt(i).AL;
            List<ISE_TEST_V1.Models.Answer> my_ans = Model.ElementAt(i).A;
            ISE_TEST_V1.Models.Mark my_form_mark = Model.ElementAt(i).db_mark;
            int a_loop = my_ans.Count();
            String bg = "";
            if (i % 2 == 0)
            {
                bg = "FFFFFF";
            }
            else
            {
                bg = "e8eef4";
            }
       
       %>
       <div style="background-color:#<%= bg %>">
       <div style="background-color:#5c87b2; color:White ; font-weight: bold; font-size:medium; text-indent: 2.0em">
  
      <br /><p>
  Question No. <%: i+1 %> </p><p> Question ID <%: my_q.ID%></p><p> <%= System.Web.HttpUtility.HtmlDecode(my_q.Text)%> 
</p><br /> <p>The Question Wight is <%: my_q.Wight%></p><br />

       </div><br /><div style="background-color:#CCFFCC; color:Black ; font-weight: bold; font-size:medium; text-indent: 2.0em"><br /><p> The correct answers in the database for that question are:</p><br />
       <% for (int j = 0; j < a_loop; j++)
          {
              ISE_TEST_V1.Models.Answer ans = my_ans.ElementAt(j);  %>
              <%: Html.HiddenFor(m => m[i].A[j].A_Order)%>
              <%: Html.HiddenFor(m => m[i].A[j].ID)%>
              <%: Html.HiddenFor(m => m[i].A[j].is_Right)%>
               <%: Html.HiddenFor(m => m[i].A[j].PID)%>
               <%: Html.HiddenFor(m => m[i].A[j].Q_ID)%>
               <% string text = System.Web.HttpUtility.HtmlEncode(Model[i].A[j].Text);
                  string fname = "[" + i + "]." + "A[" + j + "]." + "Text";
               %>
                               
              <%// <%: Html.Hidden(fname,text)%>
                 <%: Html.HiddenFor(m => m[i].A[j].value)%>
             <div style="background-color:#CCFFCC; color:Black ; font-weight: bold; font-size:medium; text-indent: 2.0em"><p>Answer No. <%: j + 1 %></p><%= System.Web.HttpUtility.HtmlDecode(ans.Text) %></div></div><br />
       <% } %>
<br /><div style="background-color:#8C0D1B; color:White ; font-weight: bold; font-size:medium; text-indent: 2.0em">The user answer for that question is:<br /></div>
<div style="background-color:#8C0D1B; color:White ; font-weight: bold; font-size:medium; text-indent: 2.0em"><br /><%= System.Web.HttpUtility.HtmlDecode(my_al.Text)%>  <br /></div><br />
<div style="background-color:#e8eef4; color:#034af3 ; font-weight: bold; font-size:medium">Enter the mark for that answer</div>
        <%: Html.TextBoxFor(m => m[i].db_mark.User_Mark)%>
        <%: Html.HiddenFor(m => m[i].AL.A_ID)%>
        <%: Html.HiddenFor(m => m[i].AL.C_T_R_ID)%>
        <%: Html.HiddenFor(m => m[i].AL.Date_Submitted)%>
        <%: Html.HiddenFor(m => m[i].AL.ID)%>
        <%: Html.HiddenFor(m => m[i].AL.is_answered)%>
        <%: Html.HiddenFor(m => m[i].AL.is_marked)%>
        <%: Html.HiddenFor(m => m[i].AL.is_right)%>
        <%: Html.HiddenFor(m => m[i].AL.is_wrong)%>
        <%: Html.HiddenFor(m => m[i].AL.need_rev)%>
        <%: Html.HiddenFor(m => m[i].AL.PID)%>
        <%: Html.HiddenFor(m => m[i].AL.Q_ID)%>
        <% String text_AL = System.Web.HttpUtility.HtmlEncode(Model[i].AL.Text);
           string fname2 = "[" + i + "]." + "AL."  + "Text";%> 
        <%//<%: Html.Hidden(fname2,text_AL)%>
        <%: Html.HiddenFor(m => m[i].AL.value)%>
        <%: Html.HiddenFor(m => m[i].db_mark.Q_ID)%>
        <%: Html.HiddenFor(m => m[i].db_mark.AL_ID)%>
        <%: Html.HiddenFor(m => m[i].db_mark.C_T_R_ID)%>
        <%: Html.HiddenFor(m => m[i].Q.ID)%>
        <%: Html.HiddenFor(m => m[i].Q.N_Answers)%>
        <% String text_Q = System.Web.HttpUtility.HtmlEncode(Model[i].Q.Text);
            string fname3 = "[" + i + "]." + "A[" + i + "]." + "Text";%>
        <% //<%: Html.Hidden(fname3,text_Q)%>
        <%: Html.HiddenFor(m => m[i].Q.Type)%>
        <%: Html.HiddenFor(m => m[i].Q.Wight)%>

        <br />        
        <%} %>
        
        <p>
                <input type="submit" value="Mark this test" />
            </p>
     <%Html.EndForm();%></div>
        <%} %>

</asp:Content>
