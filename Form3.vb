Imports MySql.Data.MySqlClient

Public Class Form3
    ' Connection (kept for future use if needed)
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=password1234567890//;database=banking_system;")

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUserControl(New UC_Dashboard())
    End Sub

    ' Load any UserControl into the content area
    Public Sub LoadUserControl(ctrl As UserControl)
        MainContentPanel.Controls.Clear()
        ctrl.Dock = DockStyle.Fill
        MainContentPanel.Controls.Add(ctrl)
    End Sub

    ' Navigation Buttons
    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        LoadUserControl(New UC_Customers()) ' Replace with a UserControl version if available
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        LoadUserControl(New UC_Accounts())
    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        LoadUserControl(New UC_Transactions())
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        LoadUserControl(New UC_Loans())
    End Sub

    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        LoadUserControl(New UC_Investments())
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        LoadUserControl(New UC_Dashboard())
    End Sub

    Private Sub Guna2Button8_Click(sender As Object, e As EventArgs) Handles Guna2Button8.Click
        ' LoadUserControl(New UC_Settings())
    End Sub
End Class
