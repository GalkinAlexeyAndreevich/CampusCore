using CampusCore.Tools.Types;
using Npgsql;

namespace CampusCore.Tools.Utils;

public static class DatabaseUtils
{
	private static String? _connectionString;

	public static void Configure(String connectionString)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
		_connectionString = connectionString;
	}

	public static Int32 Execute(String sql, Action<NpgsqlParameterCollection> getParameters) =>
		UseSqlCommand(sql, getParameters, (command) => command.ExecuteNonQuery());

	public static T[] GetAll<T>(
		String sql,
		Action<NpgsqlParameterCollection> getParameters,
		Func<NpgsqlDataReader, T> mapper
	)
	{
		return UseSqlCommand<T[]>(
			sql,
			getParameters,
			(command) =>
			{
				using NpgsqlDataReader reader = command.ExecuteReader();

				List<T> values = [];
				while (reader.Read())
					values.Add(mapper(reader));

				return [.. values];
			}
		);
	}

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
		using NpgsqlConnection connection = new(
			_connectionString ?? throw new InvalidOperationException("DatabaseUtils is not configured.")
		);
		connection.Open();

		using NpgsqlCommand command = new();
		command.Connection = connection;
		command.CommandText = sql;

		getParameters(command.Parameters);

		return getCommand(command);
	}
}
