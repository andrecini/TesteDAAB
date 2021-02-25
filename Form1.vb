Imports System.Data.SqlClient
Imports System.Text

Public Class Form1 

    Dim connString As String
    Public conexaoAccess As IDbConnection 
    Public dr As SqlDataReader
    Public Dim Daab As ApplicationBlocks.Data.SqlDbHelper = New ApplicationBlocks.Data.SqlDbHelper()
    Public Dim cnString As String = "Data Source=DB2012\SQLEXPRESS2008R2;Initial Catalog=@SQL_inVB;Persist Security Info=True;User ID=sa;Password=admin"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '''connString = "Data Source=DB2012\SQLEXPRESS2008R2;Initial Catalog=@SQL_inVB;Persist Security Info=True;User ID=sa;Password=admin"
        '''conexaoAccess.ConnectionString = connString

        TesteConnection()
    End Sub

    #Region "Botões"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conexaoAccess.Open()
        '''Dim str As String
        '''str = "SELECT * FROM Produtos WHERE (Codigo = " & Convert.ToInt32(txtCodigo.Text) & ")"
        ''' Dim cmd As SqlCommand = New SqlCommand(str, conexaoAccess)
        '''dr = cmd.ExecuteReader
        '''
        
        TesteSelect()

        While dr.Read()
            Try
            txtDescricao.Text = dr("Descricao").ToString
            txtCusto.Text = String.Format("{0:C2}", dr("Custo"))
            txtVenda.Text = String.Format("{0:C2}", dr("Venda"))
            Catch
                MessageBox.Show("ERROR!!!!!!!!!!!!!!!!!!!!!!!!!")
            End Try
        End While
        conexaoAccess.Close()
    End Sub

    Private Sub btnSair_Click(sender As Object, e As EventArgs) Handles btnSair.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        conexaoAccess.Open()

        TesteInsert()

        PadraoInicial()

        conexaoAccess.Close()
    End Sub

    Private Sub btnAtualizar_Click(sender As Object, e As EventArgs) Handles btnAtualizar.Click
        conexaoAccess.Open()

        TesteUpdate()

        PadraoInicial()

        conexaoAccess.Close()
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        conexaoAccess.Open()

        TesteRemove()

        PadraoInicial()

        conexaoAccess.Close()
    End Sub

    Private Sub btnNewDS_Click(sender As Object, e As EventArgs) Handles btnNewDS.Click
        conexaoAccess.Open()

        TesteDataSet()

        PadraoInicial()

        conexaoAccess.Close()
    End Sub
    #End Region

    #Region "Testes DAAB"
    Private Sub TesteConnection()
        
        conexaoAccess = Daab.NewConnection(cnString)
    End Sub

    Private Sub TesteSelect() 
        Dim sql As New StringBuilder
       
        sql.AppendLine("SELECT * FROM Produtos")
        sql.AppendLine("WHERE Codigo = @Codigo")
        Dim param = Daab.NewParameter("@Codigo", Convert.ToInt32(txtCodigo.Text))
        
    End Sub

    Private Sub TesteInsert()
        Dim sql As New StringBuilder
        sql.AppendLine("INSERT INTO Produtos VALUES ")
        sql.AppendLine("(@Codigo, @Descricao, @Custo, @Venda)")
        
        Dim paramContact(3) As IDataParameter

        paramContact(0) = Daab.NewParameter("@Codigo", DbType.Int32)
        paramContact(0).Value = Convert.ToInt32(txtCodigo.Text)

        paramContact(1) = Daab.NewParameter("@Descricao", DbType.AnsiString)
        paramContact(1).Value = txtDescricao.Text
        
        paramContact(2) = Daab.NewParameter("@Custo", DbType.Double)
        paramContact(2).Value = Convert.ToDouble(txtCusto.Text)

        paramContact(3) = Daab.NewParameter("@Venda", DbType.Double)
        paramContact(3).Value = Convert.ToDouble(txtVenda.Text)

        Daab.ExecuteNonQuery(cnString, CommandType.Text, sql.ToString(), paramContact)

    End Sub

    Private Sub TesteUpdate()
        Dim sql As New StringBuilder
        sql.AppendLine("UPDATE Produtos SET Venda = @Venda, Custo = @Custo WHERE (Codigo = @Codigo)")
        
        Dim paramContact(2) As IDataParameter

        paramContact(0) = Daab.NewParameter("@Venda", DbType.Double)
        paramContact(0).Value = Convert.ToDouble(txtVenda.Text)

        paramContact(1) = Daab.NewParameter("@Custo", DbType.Double)
        paramContact(1).Value = Convert.ToDouble(txtCusto.Text)

        paramContact(2) = Daab.NewParameter("@Codigo", DbType.Int32)
        paramContact(2).Value = Convert.ToInt32(txtCodigo.Text)

        Daab.ExecuteNonQuery(cnString, CommandType.Text, sql.ToString(), paramContact)
    End Sub

    Private Sub TesteRemove()
        Dim sql As New StringBuilder
        sql.AppendLine("DELETE FROM Produtos")
        sql.AppendLine("WHERE Codigo = @Codigo")
        Dim param = Daab.NewParameter("@Codigo", Convert.ToInt32(txtCodigo.Text))
        dr = Daab.ExecuteReader(cnString, CommandType.Text, sql.ToString(), param)
    End Sub

    Private Sub TesteDataSet()
        Dim ds As DataSet
        Dim sql As New StringBuilder
        Dim testeDs As Int16 = 0

        sql.AppendLine("SELECT * FROM Produtos")
        sql.AppendLine("WHERE Codigo = @Codigo")
        Dim param = Daab.NewParameter("@Codigo", Convert.ToInt32(txtCodigo.Text))
        ds = Daab.ExecuteDataset(cnString, CommandType.Text, sql.ToString(), param)

        testeDs += 1 

        Console.WriteLine(testeDs)
    End Sub

#End Region

    Private Sub PadraoInicial()
        txtCodigo.Text = ""
        txtCusto.Text = ""
        txtDescricao.Text = ""
        txtVenda.Text = ""
    End Sub

End Class
