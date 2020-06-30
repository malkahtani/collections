<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<ISE_TEST_V1.ViewModels.TF_VM>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	TF_Add
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>TF_Add</h2>
     <% if (Model != null)
        { %>
    <h3>The Submitted Values</h3>
    <ul>
    
        <% 
            int j = Model.Count();
         for (int i = 0; i < j ; i++)
         { %>
            <li>NOA<%: Model.ElementAt(i).NOA%>, TID<%: Model.ElementAt(i).TID%>, QT<%: Model.ElementAt(i).qt%></li>
            <li> QText <%: Model.ElementAt(0).tf.q.Text%></li>
            <li> Wight <%: Model.ElementAt(0).tf.q.Wight%></li>
            <li> TF Text <%: Model.ElementAt(i).tf.my_tf_answers.Text%></li>
            <li> TF Text <%: Model.ElementAt(i).tf.my_tf_answers.is_right%></li>
            <li><%: i %></li>
        <% } %>
    </ul>
      <h3>Model != null</h3>
      <li><%: j %></li>
        <% } else { %>
             <h3>Model == null</h3>
      <%  }  %>
   
</asp:Content>