<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeWF.aspx.cs" Inherits="ComprasWebApp.HomeWF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title> Aplicación - Compras </title>
    
</head>
<body>
    <form id="frHome" runat="server">
    <div>

        <h1 style="text-align:center;"> <b>  Aplicación Web Asp .Net - Ing. Nestor Estrada Olivares </b></h1>
        <hr />
        <table>

            <tr>
                <td> Número de Compra: </td>
                <td> <asp:TextBox ID="txtNumCompra" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td> Monto de Compra: </td>
                <td> <asp:TextBox ID="txtMontoCompra" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td> Nombre del Cliente: </td>
                <td> <asp:TextBox ID="txtNombreCliente" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Button ID="btnGuardar" runat="server" Text="Guardar"/>
                </td>
            </tr>

        </table>

        <hr />

        <h3> Resumen de Compras </h3>

        <asp:Label ID="lblNumeroCompraLB" runat="server" Text="Filtrar por Número de Compra: "></asp:Label>

        <asp:DropDownList ID="ddlNumeroCompra" runat="server" autopostback="True" ></asp:DropDownList>

        
        <asp:LinkButton ID="btnQuitarFiltros" runat="server" Text="Quitar Filtro"></asp:LinkButton>

        <hr />

        <asp:GridView ID="gvCompras" runat="server" AutoGenerateColumns="false" DataKeyNames="IDCompras"
                        OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"
                        OnRowDeleting="OnRowDeleting" OnRowUpdating="OnRowUpdating" EmptyDataText="No records has been added."     
                      >
        <Columns>
            <asp:TemplateField HeaderText="ID Compra" ItemStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="lblIDCompras" runat="server" Text='<%# Eval("IDCompras") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtIDCompras" runat="server" Text='<%# Eval("IDCompras") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Numero de Compra" ItemStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="lblNumeroCompra" runat="server" Text='<%# Eval("NumeroCompra") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNumeroCompra" runat="server" Text='<%# Eval("NumeroCompra") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Monto de la Compra" ItemStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="lblMontoCompra" runat="server" Text='<%# Eval("MontoCompra") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtMontoCompra" runat="server" Text='<%# Eval("MontoCompra") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Nombre del Cliente" ItemStyle-Width="150">
                <ItemTemplate>
                    <asp:Label ID="lblNombreCliente" runat="server" Text='<%# Eval("NombreCliente") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNombreCliente" runat="server" Text='<%# Eval("NombreCliente") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="150"/>
        </Columns>
        </asp:GridView>



        
    </div>
    </form>
</body>
</html>
