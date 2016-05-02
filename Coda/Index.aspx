<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Coda.Index" %>


<!DOCTYPE html>  
  
<html xmlns="http://www.w3.org/1999/xhtml">  
<head runat="server">  
    <title></title>  
    <link href="Content/bootstrap.min.css" rel="stylesheet" />  
    <link href="Content/star-rating.css" rel="stylesheet" />  
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.2.3/jquery.min.js"></script>  
    <script src="Scripts/star-rating.js"></script>  
  
    <script>  
        $(document).ready(function () {  
            $("#input-21b").on("rating.change", function (event, value, caption) {  
                  
                var ratingValue = $('#<%=hdfRatingValue.Controls%>').val();  
                ratingValue = value;  
                alert(ratingValue);  
            });  
        });  
    </script>  
</head>  
<body>  
    <form id="form1" runat="server">  
    <div>  
         
        <div class="row">  
<div class="col-lg-12">

    <input id="input-21a" value="0" type="number" class="rating" data-symbol="*" min="0" max="5" step="0.5" data-size="xl"/>
    <hr/>
    <input id="input-21b" type="number" class="rating" min="0" max="5" step="0.5" data-glyphicon="false" data-star-captions="{}" data-default-caption="{rating} Stars" data-size="lg"/>
    <hr/>
    <input id="input-21c" value="0" type="number" class="rating" min="0" max="8" step="0.5" data-size="xl" data-stars="8"/>
    <hr/>
    <input id="input-21d" value="2" type="number" class="rating" min="0" max="5" step="0.5" data-size="sm"/>
    <hr/>
    <input id="input-21e" value="0" type="number" class="rating" min="0" max="5" step="0.5" data-size="xs"/>
    <hr/>  
    </div>  
        </div>  
      
    </div>  
         <asp:HiddenField ID="hdfRatingValue" runat="server" />  
    </form>  
</body>  
</html>  