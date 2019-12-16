using System;
using Xunit;
using Hz.Infrastructure.DB;
using System.Threading.Tasks;

namespace Hz.Test
{
    public class DBUnitTest
    {
        private readonly IUnitOfWork uow;
        public DBUnitTest()
        {
            uow = new UnitOfWork((config) => {
                config.DbType = DbType.MySql;
                config.ConnectString = "server=localhost;user id=root;persistsecurityinfo=True;database=cc;port=3306;password=ztz0570@;Max Pool Size=1000";
            });
        }
        [Fact]
        public void TestExecuteInTrans()
        {
            var change = 0;
            uow.ExecuteInTrans(()=> {
                var sql = "UPDATE user SET nickname='hztest'  WHERE id = @id";
                var id = 2;
                change = uow.Execute(sql,new{id});
                sql = "UPDATE role SET name='hz123test' WHERE id = @id";
                var role = uow.ExecuteScalar(sql,new{id});
            });

            Assert.True(change > 0);
        }
        [Fact]
        public void TestQueryFirst()
        {
            var sql = "SELECT * FROM user WHERE id > @id";
            var id = 1;
            var result = uow.QueryFirst(sql,new { id });
            
            Assert.True(result?.id != null);
        }
        [Fact]
        public async Task TestQueryFirstAsync()
        {
            var sql = "SELECT * FROM user WHERE id > @id";
            var id = 1;
            var result = await uow.QueryFirstAsync(sql,new { id });
            
            Assert.True(result?.id != null);
        }

        [Fact]
        public async Task TestQueryFirstAsync_class()
        {
            var sql = "SELECT * FROM user WHERE id > @id";
            var id = 1;
            var result = await uow.QueryFirstAsync<user>(sql,new { id });
            
            Assert.True(!string.IsNullOrWhiteSpace(result.username));
        }

        [Fact]
        public void TestQuerySingle()
        {
            var sql = "SELECT * FROM user WHERE id > @id";
            var id = 10;
            var result = uow.QuerySingleOrDefault(sql,new{id});
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestQuerySingleAsync()
        {
            var sql = "SELECT * FROM user WHERE id=@id";
            var id = 1;
            var result =await uow.QueryFirstOrDefaultAsync<user>(sql,new{id});
            Assert.Null(result);
        }

        [Fact]
        public async Task TestQueryAsync()
        {
            var sql = "SELECT * FROM user";
            var result =await uow.QueryAsync<user>(sql);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task ExecuteScalar_ClassAsync()
        {
            var sql = "SELECT avatar FROM user where id = 2";
            var result = await uow.ExecuteScalarAsync<string>(sql);
            Assert.True(result != null);
        }
    }
}
