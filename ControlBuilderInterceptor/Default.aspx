<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ControlBuilderInterceptor._Default" %>
<%@ Register TagPrefix="ww" Namespace="MsdnMag.Web.Controls" Assembly="wwDataBinder" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >

    <asp:DropDownList runat="server" ID="ddl1" SelectMethod="GetFirst" ItemType="System.Int32" AutoPostBack="True"
        SelectedValue="<%# Model.Int1 %>" EnableViewState="False"/>
    <br />
    <asp:TextBox runat="server" id="tb1" Text="<%# ddl1.SelectedValue %>" AutoBind="True" />

    <asp:DropDownList runat="server" ID="ddl2" SelectMethod="GetSecond" ItemType="System.Int32" AutoPostBack="True"
        SelectedValue="<%# Model.Int2 %>" EnableViewState="False">
    </asp:DropDownList>
    
    <asp:Button runat="server" Text="submit" OnClick="OnClick"/>
    <%--<% error %>--%>
    <ww:wwDataBinder runat="server" ID="Binder">
        <DataBindingItems>
            <ww:wwDataBindingItem runat="server" ControlId="ddl1" BindingMode="TwoWay" BindingSource="Model" BindingSourceMember="Int1" BindingProperty="SelectedValue"></ww:wwDataBindingItem>
            <ww:wwDataBindingItem runat="server" ControlId="tb1" BindingMode="OneWay" BindingSource="Model" BindingSourceMember="Int1" BindingProperty="Text"></ww:wwDataBindingItem>
            <ww:wwDataBindingItem runat="server" ControlId="ddl2" BindingMode="TwoWay" BindingSource="Model" BindingSourceMember="Int2" BindingProperty="SelectedValue"></ww:wwDataBindingItem>
        </DataBindingItems>
    </ww:wwDataBinder>
</asp:Content>
