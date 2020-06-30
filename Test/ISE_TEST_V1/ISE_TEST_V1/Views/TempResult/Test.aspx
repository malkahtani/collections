<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ISE_TEST_V1.ViewModels.T_Detaile>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Test Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Exam For Printe</h2>
    <% int[] qtype1 = new int[2] { 1, 2 };
       
       int count = Model.Q.Count;
       for (int i = 0; i < count; i++)
       {
           int qtype = Model.Q.ElementAt(i).Type;
           if (qtype1.Contains(qtype))
           {
                %> 
                <table>
                <tr>
                <td>  Question No</td> 
                <td>Question ID </td>
                <td>The Question Wight</td>
                <td>The Question Type</td>
                </tr>
                <tr><td><%: i + 1%> </td> 
                <td><%: Model.Q.ElementAt(i).ID%></td> 
                <td> <%: Model.Q.ElementAt(i).Wight%></td>
                <td> <%: ISE_TEST_V1.Models.QType.Type[Model.Q.ElementAt(i).Type]%></td>
                </tr>
                <tr><td>The Question Text</td></tr>
                <tr> <td>
                
<%= System.Web.HttpUtility.HtmlDecode(Model.Q.ElementAt(i).Text)%>
</td>
</tr>

       <tr><td>The answers in the database for this question are:</td></tr>
       <% for (int j = 0; j < Model.Answers.ElementAt(i).my_List.Count; j++)
          {
              ISE_TEST_V1.Models.Answer ans = Model.Answers.ElementAt(i).my_List.ElementAt(j);  %>
             
               <% string text = System.Web.HttpUtility.HtmlEncode(ans.Text);
                  if (Model.U_Answers.ElementAt(i).my_List.ElementAt(0).ID == ans.ID)
                  {
               %>
                               
            
            <tr><td>Answer No <%: j + 1%></td><td><%= System.Web.HttpUtility.HtmlDecode(ans.Text)%></td><td>This Answer Selected by Candidate</td></tr>
       <% }
                  else
                  {%>
       <tr><td>Answer No <%: j + 1%></td><td><%= System.Web.HttpUtility.HtmlDecode(ans.Text)%></td></tr>
       <%}
           %>

<% 
}

          ISE_TEST_V1.Models.Answer ans1 = Model.Answers.ElementAt(i).my_List.Find(delegate(ISE_TEST_V1.Models.Answer _myans)
  {

      return _myans.ID == Model.U_Answers.ElementAt(i).my_List.ElementAt(0).ID;

  });
          if (ans1 != null)
          {
   %>


     <%}
          else
          { %>
    

    
     <%} %>    
      </table>  
      <br />
           <%} else if (qtype == 3)
           {
                %> 
                <table>
                <tr>
                <td>  Question No</td> 
                <td>Question ID </td>
                <td>The Question Wight</td>
                <td>The Question Type</td>
                </tr>
                <tr><td><%: i + 1%> </td> 
                <td><%: Model.Q.ElementAt(i).ID%></td> 
                <td> <%: Model.Q.ElementAt(i).Wight%></td>
                <td> <%: ISE_TEST_V1.Models.QType.Type[Model.Q.ElementAt(i).Type]%></td>
                </tr>
                <tr><td>The Question Text</td></tr>
                <tr> <td>
                
<%= System.Web.HttpUtility.HtmlDecode(Model.Q.ElementAt(i).Text)%>
</td>
</tr>

       <tr><td>The answers in the database for this question are:</td></tr>
       <% 
                 
                 for (int j = 0; j < Model.Answers.ElementAt(i).my_List.Count; j++)
          {
              ISE_TEST_V1.Models.Answer ans = Model.Answers.ElementAt(i).my_List.ElementAt(j);
                      %>
              
               <%  string text = System.Web.HttpUtility.HtmlEncode(ans.Text);
               
                     
               
                      ISE_TEST_V1.Models.Answer mcans = Model.U_Answers.ElementAt(i).my_List.Find(delegate(ISE_TEST_V1.Models.Answer _mcans1)
  {

      return _mcans1.ID == ans.ID;

  });
          if (mcans != null)
          {
   %>
    <tr><td>Answer No <%: j + 1%></td><td><%= System.Web.HttpUtility.HtmlDecode(ans.Text)%></td><td>This Answer Selected by Candidate</td></tr>

     <%}
          else
          { %>
    
     <tr><td>Answer No <%: j + 1%></td><td><%= System.Web.HttpUtility.HtmlDecode(ans.Text)%></td></tr>
    
     <%} %>    
               
                 <%

}

          %>
      </table>  
      <br />
           <%}
           else if (qtype == 4)
           {%>
                <table>
                <tr>
                <td>  Question No</td> 
                <td>Question ID </td>
                <td>The Question Wight</td>
                <td>The Question Type</td>
                </tr>
                <tr><td><%: i + 1%> </td> 
                <td><%: Model.Q.ElementAt(i).ID%></td> 
                <td> <%: Model.Q.ElementAt(i).Wight%></td>
                <td> <%: ISE_TEST_V1.Models.QType.Type[Model.Q.ElementAt(i).Type]%></td>
                </tr>
                <tr><td>The Question Text</td></tr>
                <tr> <td>
                
<%= System.Web.HttpUtility.HtmlDecode(Model.Q.ElementAt(i).Text)%>
</td>
</tr>

       
       <% 
           bool got = false;
                 
           for (int j = 0; j < Model.Answers.ElementAt(i).my_List.Count; j++)
           {
               ISE_TEST_V1.Models.Answer ans = Model.Answers.ElementAt(i).my_List.ElementAt(j);
                      %>
              
               <%  string text = System.Web.HttpUtility.HtmlEncode(ans.Text);



                   ISE_TEST_V1.Models.Answer fgstr = Model.U_Answers.ElementAt(i).my_List.Find(delegate(ISE_TEST_V1.Models.Answer _fgstr)
{

    return _fgstr.ID == ans.ID;

});
                   if (fgstr != null)
                   {
                       got = true;
   %>
    <tr><td>Answer No <%: j + 1%></td><td><%= System.Web.HttpUtility.HtmlDecode(ans.Text)%></td><td>This Answer Selected by Candidate</td></tr>

     <%}
                  
           }
            if (got == false)
         {
                   %>
                   <tr><td>Candidate Answer is different from the answers in the database:</td><td><%: Model.U_Answers.ElementAt(i).my_List.ElementAt(0).Text%></td></tr>
                   <%}
           %>
           </table>
            <br />
           <%}
           
           else if (qtype == 5)
           {%>
                <table>
                <tr>
                <td>  Question No</td> 
                <td>Question ID </td>
                <td>The Question Wight</td>
                <td>The Question Type</td>
                </tr>
                <tr><td><%: i + 1%> </td> 
                <td><%: Model.Q.ElementAt(i).ID%></td> 
                <td> <%: Model.Q.ElementAt(i).Wight%></td>
                <td> <%: ISE_TEST_V1.Models.QType.Type[Model.Q.ElementAt(i).Type]%></td>
                </tr>
                <tr><td>The Question Text</td></tr>
                <tr> <td>
                
<%= System.Web.HttpUtility.HtmlDecode(Model.Q.ElementAt(i).Text)%>
</td>
</tr>

       
       <% 
           bool got = false;
           for (int j = 0; j < Model.Answers.ElementAt(i).my_List.Count; j++)
           {
               ISE_TEST_V1.Models.Answer ans = Model.Answers.ElementAt(i).my_List.ElementAt(j);
                      %>
              
               <%  



                   ISE_TEST_V1.Models.Answer fgint = Model.U_Answers.ElementAt(i).my_List.Find(delegate(ISE_TEST_V1.Models.Answer _fgint)
{

    return _fgint.ID == ans.ID;

});
                   if (fgint != null)
                   {
                       got = true;
   %>
    <tr><td>Answer No <%: j + 1%></td><td><%= ans.value%></td><td>This Answer Selected by Candidate</td></tr>
     
     <%}
                  
           }
         if (got == false)
         {
                   %>
                   <tr><td>Candidate Answer is different from the answers in the database:</td><td><%: Model.U_Answers.ElementAt(i).my_List.ElementAt(0).value%></td></tr>
                   <%}
               
           %>
           </table>
            <br />
           <%}
           
           else if (qtype == 6)
           {%>
             <table>
                <tr>
                <td>  Question No</td> 
                <td>Question ID </td>
                <td>The Question Wight</td>
                <td>The Question Type</td>
                </tr>
                <tr><td><%: i + 1%> </td> 
                <td><%: Model.Q.ElementAt(i).ID%></td> 
                <td> <%: Model.Q.ElementAt(i).Wight%></td>
                <td> <%: ISE_TEST_V1.Models.QType.Type[Model.Q.ElementAt(i).Type]%></td>
                </tr>
                <tr><td>The Question Text</td></tr>
                <tr> <td>
                <%= System.Web.HttpUtility.HtmlDecode(Model.Q.ElementAt(i).Text)%>
</td>
</tr>
            <tr>
            <td>The Candidate Answer</td>
            </tr>
            <% 
List<ISE_TEST_V1.Models.Answer> gresult = Model.Answers.ElementAt(i).my_List.FindAll(delegate(ISE_TEST_V1.Models.Answer _gmyans)
{

    return _gmyans.PID > 0;

});

List<ISE_TEST_V1.Models.Answer> result = Model.U_Answers.ElementAt(i).my_List.FindAll(delegate(ISE_TEST_V1.Models.Answer _myans)
{

    return _myans.PID > 0;

});
if (result != null)
{
    
     
    for (int ind = 0; ind < result.Count; ind++)
    {
        
       
            int pid = result.ElementAt(ind).PID.Value;
            
        int l_index = 0;
            ISE_TEST_V1.Models.Answer u_ans = result.ElementAt(ind);
            for (int w=0 ; w < gresult.Count ; w++){
            if(gresult.ElementAt(w).ID == u_ans.ID){
                l_index = w;
                gresult.RemoveAt(l_index);
            }
            }
                ISE_TEST_V1.Models.Answer pid_ans = Model.Answers.ElementAt(i).my_List.Find(delegate(ISE_TEST_V1.Models.Answer _myans10)
        {

            return _myans10.ID == u_ans.ID;

        });
                
                
            
           
         
            ISE_TEST_V1.Models.Answer pid_ans1 = Model.Answers.ElementAt(i).my_List.Find(delegate(ISE_TEST_V1.Models.Answer _myans1)
        {

            return _myans1.ID == pid;

        });
        
        
        if (pid_ans1 != null)
        {
            string mt_text = pid_ans.Text;
            string mt1_text = pid_ans1.Text;
        %>
        <tr><td><%= System.Web.HttpUtility.HtmlDecode(mt1_text)%></td> <td> Linked With </td><td><%= System.Web.HttpUtility.HtmlDecode(mt_text)%></td></tr>
        
   <% }else{
   %>
    <tr>
            <td><%= gresult.Count %> </td> <td><%= result.Count %> </td> <td><%= pid %> </td> 
            </tr>
   <%
   }
           
    }



for(int w = 0 ; w <gresult.Count ; w++){
 string text = gresult.ElementAt(w).Text; %>
 <tr><td><%= System.Web.HttpUtility.HtmlDecode(text)%></td><td> Has Not Linked to any statment</td></tr>
 <%
}
}
else
{ %>
    <tr>
            <td>The Candidate Answer is Null</td>
            </tr>

    
     <%} %>  
                 
            </table>
            <br /> 
                    
            
           <%}
           else if (qtype == 8)
           {
               %>
               <table>
                <tr>
                <td>  Question No</td> 
                <td>Question ID </td>
                <td>The Question Wight</td>
                <td>The Question Type</td>
                </tr>
                <tr><td><%: i + 1%> </td> 
                <td><%: Model.Q.ElementAt(i).ID%></td> 
                <td> <%: Model.Q.ElementAt(i).Wight%></td>
                <td> <%: ISE_TEST_V1.Models.QType.Type[Model.Q.ElementAt(i).Type]%></td>
                </tr>
                <tr><td>The Question Text</td></tr>
                
               <tr><td> 
                
<%= System.Web.HttpUtility.HtmlDecode(Model.Q.ElementAt(i).Text)%>
</td>
</tr>

     
      
               <tr><td>Candidate Answer </td><td><%= System.Web.HttpUtility.HtmlDecode(Model.U_Answers.ElementAt(i).my_List.ElementAt(0).Text)%></td></tr>
          
           </table>
            <br />
           
           
                      <%}
           else if (qtype == 7)
           {%>
                <table>
                <tr>
                <td>  Question No</td> 
                <td>Question ID </td>
                <td>The Question Wight</td>
                <td>The Question Type</td>
                </tr>
                <tr><td><%: i + 1%> </td> 
                <td><%: Model.Q.ElementAt(i).ID%></td> 
                <td> <%: Model.Q.ElementAt(i).Wight%></td>
                <td> <%: ISE_TEST_V1.Models.QType.Type[Model.Q.ElementAt(i).Type]%></td>
                </tr>
                <tr><td>The Question Text</td></tr>
                <tr> <td>
                
<%= System.Web.HttpUtility.HtmlDecode(Model.Q.ElementAt(i).Text)%>
</td>
</tr>

       <tr><td>The answers in the database for this question are:</td></tr>
       <% bool got = false;
          for (int j = 0; j < Model.Answers.ElementAt(i).my_List.Count; j++)
          {
              ISE_TEST_V1.Models.Answer ans = Model.Answers.ElementAt(i).my_List.ElementAt(j);  %>
              
               <% 
if (Model.U_Answers.ElementAt(i).my_List.ElementAt(0).ID == ans.ID)
{
    got = true;
               %>
                               
            
            <tr><td>Answer No <%: j + 1%></td><td><%= ans.Text%></td><td>This Answer Selected by Candidate</td></tr>
       <% }
else
{%>
       
       <%}
          }
          if (got == false)
          {%>
               <tr><td>Candidate Answer is different from the answers in the database:</td><td><%= System.Web.HttpUtility.HtmlDecode(Model.U_Answers.ElementAt(i).my_List.ElementAt(0).Text)%></td></tr>
          <% }
           %>
           </table>
            <br />
           
            
            <%}
       } %>
</asp:Content>