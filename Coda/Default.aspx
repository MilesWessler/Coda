<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Coda.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-2.2.3.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".rating-star-block .star").mouseleave(function () {
                $("#" + $(this).parent().attr('id') + " .star").each(function () {
                    $(this).addClass("outline");
                    $(this).removeClass("filled");
                });
            });
            $(".rating-star-block .star").mouseenter(function () {
                var hoverVal = $(this).attr('rating');
                $(this).prevUntil().addClass("filled");
                $(this).addClass("filled");
                $("#RAT").html(hoverVal);
            });
            $(".rating-star-block .star").click(function () {

                var v = $(this).attr('rating');
                var newScore = 0;
                var updateP = "#" + $(this).parent().attr('id') + ' .CurrentScore';
                var artID = $("#" + $(this).parent().attr('id') + ' .songId').val();

                $("#" + $(this).parent().attr('id') + " .star").hide();
                $("#" + $(this).parent().attr('id') + " .yourScore").html("Your Score is : &nbsp;<b style='color:#ff9900; font-size:15px'>" + v + "</b>");
                $.ajax({
                    type: "POST",
                    url: "Default.aspx/SaveRating",
                    data: "{songId: '" + artID + "',rate: '" + v + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        setNewScore(updateP, data.d);
                    },
                    error: function (data) {
                        alert(data.d);
                    }
                });
            });
        });

        function setNewScore(container, data) {
            $(container).html(data);
            $("#myElem").show('1000').delay(2000).queue(function (n) {
                $(this).hide();
                n();
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CellPadding="5">
            <Columns>
               <asp:BoundField HeaderText="Song Title" DataField="Title" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <div class="rating-star-block" id='div_<%#Container.DataItemIndex %>'>
                            <input type="hidden" class="Id" value='<%#Eval("SongId") %>' />
                            Current Score :<span class="CurrentScore"><%#Eval("Score") %></span><br />
                            <div class="yourScore">Your Score : </div>
                            <a class="star outline" href="#" rating="1" title="vote 1">vote 1</a>
                            <a class="star outline" href="#" rating="2" title="vote 2">vote 2</a>
                            <a class="star outline" href="#" rating="3" title="vote 3">vote 3</a>
                            <a class="star outline" href="#" rating="4" title="vote 4">vote 4</a>
                            <a class="star outline" href="#" rating="5" title="vote 5">vote 5</a>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div id="myElem" style="position: absolute; top: 10px; left: 50%; display: none; background-color: yellow; padding: 5px; border: 1px solid red">
            Thank you for your rating!
        </div>
    </form>
</body>
</html>
