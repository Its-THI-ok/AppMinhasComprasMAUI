using MauiAppMinhasCompras.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiAppMinhasCompras.Helper
{
    public class SQLiteDatabaseHelper {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string dbPath)
        {
            _conn = new SQLiteAsyncConnection(dbPath);
            _conn.CreateTableAsync<Models.Produto>().Wait();
        }
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }
        public Task<List<Produto>> Update(Produto p)
        {
            return _conn.QueryAsync<Produto>("UPDATE Produto SET Descricao = ?, Quantidade = ?, Preco = ? WHERE Id = ?", p.Descricao, p.Quantidade, p.Preco, p.Id);
        }
        public Task<int> Delete(int id) {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }
        public Task<List<Produto>> GetAll() {
            return _conn.Table<Produto>().ToListAsync();
        }
        public Task<List<Produto>> Search(string q) {
            // Evita desreferência nula de Descricao (CS8602)
            return _conn.Table<Produto>().Where(i => i.Descricao != null && i.Descricao.Contains(q)).ToListAsync();
        }
    }
}
