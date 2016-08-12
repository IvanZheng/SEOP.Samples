<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Sample._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<div class="row">
        <div class="col-md-4">
            <h2>Api Samples Add Product</h2>

            <p>name:<input type="text" id="productName" /></p>
            <p>count:<input type="text" id="productCount" /></p>
            <a class="btn btn-default" id="addProduct">add product </a>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <h2>Api Samples Reduce Product</h2>

            <p>productId:<input type="text" id="productId" style="width:300px" /></p>
            <p>reduce count:<input type="text" id="reduceCount" value="1" /></p>
            <input type="button" class="btn btn-default" id="reduceProduct" value="reduce product" />
        </div>
    </div>

    <div id="products" class="jumbotron">
       
    </div>
    <div id="result"></div>
    <script>
        var showResult = function (result) {
            $("#result").prepend(result + "<br>");
        }
        var products = $("#products");
        var refreshProducts = function () {
            products.empty();
             $.get("<%=ResolveUrl("~/api/values/getTopProducts")%>", {count: 5}, function (result) {
                $.each(result.result, function (i, p) {
                    $(String.format("<div><a style='cursor:pointer' data-productid='{0}'>{0}:{1} count:{2} </a></div>", p.id, p.name, p.count)).appendTo(products)
                });
                products.find("a").click(function () {
                    var productId = $(this).data("productid");
                    $("#productId").val(productId);
                });
            });
        };
        $(function () {
            refreshProducts();

            $("#addProduct").click(function () {
                var data = {
                    name: $("#productName").val(),
                    count: $("#productCount").val()
                };
                $.post("<%=ResolveUrl("~/api/values/createProduct")%>", data, function (result) {
                    showResult(JSON.stringify(result));
                    refreshProducts();
                });
            });

            $("#reduceProduct").click(function () {
                var data = {
                    productId: $("#productId").val(),
                    reduceCount: $("#reduceCount").val()
                }
                $.post("<%=ResolveUrl("~/api/values/reduceProduct")%>", data, function (result) {
                    showResult(JSON.stringify(result));
                    refreshProducts();
                });
            });
            
        });
    </script>
</asp:Content>

