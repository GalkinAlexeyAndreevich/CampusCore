using CampusCore.Tools.Types;
using Npgsql;

namespace CampusCore.Tools.Utils;

public static class DatabaseUtils
{
	private const String _connectionString = "Server=localhost;Username=postgres;Password=root;Database=campus_core";

	public static Int32 Execute(String sql, Action<NpgsqlParameterCollection> getParameters) =>
		UseSqlCommand(sql, getParameters, (command) => command.ExecuteNonQuery());

	public static Page<T> GetPage<T>(
		String sql,
		Action<NpgsqlParameterCollection> getParameters,
		Func<NpgsqlDataReader, T> mapper
	)
	{
		return UseSqlCommand(
			sql,
			getParameters,
			(command) =>
			{
				using NpgsqlDataReader reader = command.ExecuteReader();

				List<T> values = [];
				Int32 totalRows = 0;

				while (reader.Read())
				{
					totalRows = Convert.ToInt32(reader["count"]);
					values.Add(mapper(reader));
				}

				return new Page<T>([.. values], totalRows);
			}
		);
	}

	public static T? Get<T>(String sql, Action<NpgsqlParameterCollection> getParameters, Func<NpgsqlDataReader, T> mapper)
	{
		return UseSqlCommand(
			sql,
			getParameters,
			(command) =>
			{
				using NpgsqlDataReader reader = command.ExecuteReader();

				if (!reader.Read())
					return default;

				return mapper(reader);
			}
		);
	}

	private static T UseSqlCommand<T>(
		String sql,
		Action<NpgsqlParameterCollection> getParameters,
		Func<NpgsqlCommand, T> getCommand
	)
	{
		using NpgsqlConnection connection = new(_connectionString);
		connection.Open();

		using NpgsqlCommand command = new();
		command.Connection = connection;
		command.CommandText = sql;

		getParameters(command.Parameters);

		return getCommand(command);
	}
}
