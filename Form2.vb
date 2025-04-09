Imports MySql.Data.MySqlClient

Public Class Form2
    ' Define MySQL connection
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=password1234567890//;database=banking_system;")
    Dim dtAdmins As New DataTable() ' Store Admins table data in memory

    ' Load Admins table when form loads
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conn.Open()
            Dim query As String = "SELECT * FROM Admins"
            Dim adapter As New MySqlDataAdapter(query, conn)
            adapter.Fill(dtAdmins) ' Load the entire Admins table into memory
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Failed to load Admins table: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Login function with preloaded Admins table
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ' Validate input fields
        If txtEmail.Text = "" Or txtPassword.Text = "" Then
            MessageBox.Show("Enter both Email and Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Check if entered credentials match any record in preloaded Admins table
        Dim foundRows = dtAdmins.Select($"email = '{txtEmail.Text}' AND password = '{txtPassword.Text}'")

        If foundRows.Length > 0 Then
            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Form3.Show()
            Me.Hide()
        Else
            MessageBox.Show("Invalid Email or Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Button to return to previous form
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Form1.Show()
        Me.Hide()
    End Sub
End Class
