using Microsoft.Data.Sqlite;
using StroyMaterials.App.Models;

namespace StroyMaterials.App;

internal static class Database
{
    public static UserSession? Authenticate(string login, string password)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT users.id, users.full_name, roles.name
            FROM users
            JOIN roles ON roles.id = users.role_id
            WHERE users.login = $login AND users.password = $password;
            """;
        command.Parameters.AddWithValue("$login", login);
        command.Parameters.AddWithValue("$password", password);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return new UserSession
        {
            UserId = reader.GetInt32(0),
            FullName = reader.GetString(1),
            RoleName = reader.GetString(2)
        };
    }

    public static List<ProductRecord> GetProducts(string search, int? manufacturerId, string sortKey)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();

        var where = new List<string>();
        if (!string.IsNullOrWhiteSpace(search))
        {
            where.Add("""
                (products.article LIKE $search OR products.name LIKE $search OR products.description LIKE $search
                 OR categories.name LIKE $search OR manufacturers.name LIKE $search OR suppliers.name LIKE $search
                 OR units.name LIKE $search)
                """);
            command.Parameters.AddWithValue("$search", $"%{search.Trim()}%");
        }

        if (manufacturerId is not null)
        {
            where.Add("products.manufacturer_id = $manufacturerId");
            command.Parameters.AddWithValue("$manufacturerId", manufacturerId.Value);
        }

        var orderBy = sortKey switch
        {
            "stock_asc" => "products.stock_quantity ASC, products.name ASC",
            "stock_desc" => "products.stock_quantity DESC, products.name ASC",
            "price_asc" => "products.price ASC, products.name ASC",
            "price_desc" => "products.price DESC, products.name ASC",
            "discount_asc" => "products.discount ASC, products.name ASC",
            "discount_desc" => "products.discount DESC, products.name ASC",
            _ => "products.name ASC, products.article ASC"
        };

        command.CommandText = $"""
            SELECT products.article, products.name, products.unit_id, units.name,
                   products.price, products.supplier_id, suppliers.name,
                   products.manufacturer_id, manufacturers.name,
                   products.category_id, categories.name,
                   products.discount, products.stock_quantity, products.description, products.image_path
            FROM products
            JOIN units ON units.id = products.unit_id
            JOIN suppliers ON suppliers.id = products.supplier_id
            JOIN manufacturers ON manufacturers.id = products.manufacturer_id
            JOIN categories ON categories.id = products.category_id
            {(where.Count == 0 ? "" : "WHERE " + string.Join(" AND ", where))}
            ORDER BY {orderBy};
            """;

        return ReadProducts(command);
    }

    public static ProductRecord? GetProduct(string article)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT products.article, products.name, products.unit_id, units.name,
                   products.price, products.supplier_id, suppliers.name,
                   products.manufacturer_id, manufacturers.name,
                   products.category_id, categories.name,
                   products.discount, products.stock_quantity, products.description, products.image_path
            FROM products
            JOIN units ON units.id = products.unit_id
            JOIN suppliers ON suppliers.id = products.supplier_id
            JOIN manufacturers ON manufacturers.id = products.manufacturer_id
            JOIN categories ON categories.id = products.category_id
            WHERE products.article = $article;
            """;
        command.Parameters.AddWithValue("$article", article);
        return ReadProducts(command).SingleOrDefault();
    }

    public static void SaveProduct(ProductRecord product, bool isNew)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = isNew
            ? """
              INSERT INTO products (article, name, unit_id, price, supplier_id, manufacturer_id, category_id, discount, stock_quantity, description, image_path)
              VALUES ($article, $name, $unit_id, $price, $supplier_id, $manufacturer_id, $category_id, $discount, $stock_quantity, $description, $image_path);
              """
            : """
              UPDATE products
              SET name = $name,
                  unit_id = $unit_id,
                  price = $price,
                  supplier_id = $supplier_id,
                  manufacturer_id = $manufacturer_id,
                  category_id = $category_id,
                  discount = $discount,
                  stock_quantity = $stock_quantity,
                  description = $description,
                  image_path = $image_path
              WHERE article = $article;
              """;
        AddProductParameters(command, product);
        command.ExecuteNonQuery();
    }

    public static bool ProductIsUsedInOrders(string article)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM order_items WHERE product_article = $article;";
        command.Parameters.AddWithValue("$article", article);
        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    public static void DeleteProduct(string article)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM products WHERE article = $article;";
        command.Parameters.AddWithValue("$article", article);
        command.ExecuteNonQuery();
    }

    public static string GetNextProductArticle()
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) + 1 FROM products;";
        var next = Convert.ToInt32(command.ExecuteScalar());
        string article;
        do
        {
            article = $"NEW{next:D4}";
            next++;
        }
        while (ProductExists(article));

        return article;
    }

    public static bool ProductExists(string article)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM products WHERE article = $article;";
        command.Parameters.AddWithValue("$article", article);
        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    public static List<OrderRecord> GetOrders()
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT orders.id,
                   GROUP_CONCAT(order_items.product_article || ', ' || order_items.quantity, ', ') AS items_text,
                   orders.order_date, orders.delivery_date,
                   pickup_points.id, pickup_points.address,
                   orders.customer_name, orders.receive_code,
                   order_statuses.id, order_statuses.name
            FROM orders
            JOIN pickup_points ON pickup_points.id = orders.pickup_point_id
            JOIN order_statuses ON order_statuses.id = orders.status_id
            LEFT JOIN order_items ON order_items.order_id = orders.id
            GROUP BY orders.id
            ORDER BY orders.id;
            """;
        return ReadOrders(command);
    }

    public static OrderRecord? GetOrder(int id)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT orders.id,
                   GROUP_CONCAT(order_items.product_article || ', ' || order_items.quantity, ', ') AS items_text,
                   orders.order_date, orders.delivery_date,
                   pickup_points.id, pickup_points.address,
                   orders.customer_name, orders.receive_code,
                   order_statuses.id, order_statuses.name
            FROM orders
            JOIN pickup_points ON pickup_points.id = orders.pickup_point_id
            JOIN order_statuses ON order_statuses.id = orders.status_id
            LEFT JOIN order_items ON order_items.order_id = orders.id
            WHERE orders.id = $id
            GROUP BY orders.id;
            """;
        command.Parameters.AddWithValue("$id", id);
        return ReadOrders(command).SingleOrDefault();
    }

    public static void SaveOrder(OrderRecord order, IReadOnlyList<(string Article, int Quantity)> items, bool isNew)
    {
        using var connection = OpenConnection();
        using var transaction = connection.BeginTransaction();

        if (isNew)
        {
            using var nextCommand = connection.CreateCommand();
            nextCommand.Transaction = transaction;
            nextCommand.CommandText = "SELECT COALESCE(MAX(id), 0) + 1 FROM orders;";
            order.Id = Convert.ToInt32(nextCommand.ExecuteScalar());
        }

        using (var command = connection.CreateCommand())
        {
            command.Transaction = transaction;
            command.CommandText = isNew
                ? """
                  INSERT INTO orders (id, order_date, delivery_date, pickup_point_id, customer_name, receive_code, status_id)
                  VALUES ($id, $order_date, $delivery_date, $pickup_point_id, $customer_name, $receive_code, $status_id);
                  """
                : """
                  UPDATE orders
                  SET order_date = $order_date,
                      delivery_date = $delivery_date,
                      pickup_point_id = $pickup_point_id,
                      customer_name = $customer_name,
                      receive_code = $receive_code,
                      status_id = $status_id
                  WHERE id = $id;
                  """;
            command.Parameters.AddWithValue("$id", order.Id);
            command.Parameters.AddWithValue("$order_date", order.OrderDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("$delivery_date", order.DeliveryDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("$pickup_point_id", order.PickupPointId);
            command.Parameters.AddWithValue("$customer_name", order.CustomerName);
            command.Parameters.AddWithValue("$receive_code", order.ReceiveCode);
            command.Parameters.AddWithValue("$status_id", order.StatusId);
            command.ExecuteNonQuery();
        }

        using (var deleteItems = connection.CreateCommand())
        {
            deleteItems.Transaction = transaction;
            deleteItems.CommandText = "DELETE FROM order_items WHERE order_id = $id;";
            deleteItems.Parameters.AddWithValue("$id", order.Id);
            deleteItems.ExecuteNonQuery();
        }

        foreach (var item in items)
        {
            using var insertItem = connection.CreateCommand();
            insertItem.Transaction = transaction;
            insertItem.CommandText = """
                INSERT INTO order_items (order_id, product_article, quantity)
                VALUES ($order_id, $product_article, $quantity);
                """;
            insertItem.Parameters.AddWithValue("$order_id", order.Id);
            insertItem.Parameters.AddWithValue("$product_article", item.Article);
            insertItem.Parameters.AddWithValue("$quantity", item.Quantity);
            insertItem.ExecuteNonQuery();
        }

        transaction.Commit();
    }

    public static void DeleteOrder(int id)
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM orders WHERE id = $id;";
        command.Parameters.AddWithValue("$id", id);
        command.ExecuteNonQuery();
    }

    public static List<LookupItem> GetLookupItems(string tableName)
    {
        var allowed = new HashSet<string>
        {
            "categories", "units", "suppliers", "manufacturers", "pickup_points", "order_statuses"
        };
        if (!allowed.Contains(tableName))
        {
            throw new ArgumentException("Недопустимый справочник.", nameof(tableName));
        }

        var nameColumn = tableName == "pickup_points" ? "address" : "name";
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = $"SELECT id, {nameColumn} FROM {tableName} ORDER BY {nameColumn};";

        var items = new List<LookupItem>();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            items.Add(new LookupItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
        }

        return items;
    }

    public static List<string> GetCustomers()
    {
        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            SELECT DISTINCT users.full_name
            FROM users
            JOIN roles ON roles.id = users.role_id
            WHERE roles.name = 'Авторизованный клиент'
            UNION
            SELECT DISTINCT customer_name FROM orders
            ORDER BY 1;
            """;

        var customers = new List<string>();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            customers.Add(reader.GetString(0));
        }

        return customers;
    }

    public static long GetDatabaseSizeBytes() => new FileInfo(AppPaths.DatabasePath).Length;

    public static List<(string TableName, int Rows)> GetTableStats()
    {
        using var connection = OpenConnection();
        using var tablesCommand = connection.CreateCommand();
        tablesCommand.CommandText = """
            SELECT name FROM sqlite_master
            WHERE type = 'table' AND name NOT LIKE 'sqlite_%'
            ORDER BY name;
            """;

        var tableNames = new List<string>();
        using (var reader = tablesCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                tableNames.Add(reader.GetString(0));
            }
        }

        var stats = new List<(string TableName, int Rows)>();
        foreach (var table in tableNames)
        {
            using var countCommand = connection.CreateCommand();
            countCommand.CommandText = $"SELECT COUNT(*) FROM {table};";
            stats.Add((table, Convert.ToInt32(countCommand.ExecuteScalar())));
        }

        return stats;
    }

    private static SqliteConnection OpenConnection()
    {
        var connection = new SqliteConnection($"Data Source={AppPaths.DatabasePath};Foreign Keys=True");
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "PRAGMA foreign_keys = ON;";
        command.ExecuteNonQuery();
        return connection;
    }

    private static List<ProductRecord> ReadProducts(SqliteCommand command)
    {
        var products = new List<ProductRecord>();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            products.Add(new ProductRecord
            {
                Article = reader.GetString(0),
                Name = reader.GetString(1),
                UnitId = reader.GetInt32(2),
                UnitName = reader.GetString(3),
                Price = reader.GetDecimal(4),
                SupplierId = reader.GetInt32(5),
                SupplierName = reader.GetString(6),
                ManufacturerId = reader.GetInt32(7),
                ManufacturerName = reader.GetString(8),
                CategoryId = reader.GetInt32(9),
                CategoryName = reader.GetString(10),
                Discount = reader.GetInt32(11),
                StockQuantity = reader.GetInt32(12),
                Description = reader.GetString(13),
                ImagePath = reader.GetString(14)
            });
        }

        return products;
    }

    private static List<OrderRecord> ReadOrders(SqliteCommand command)
    {
        var orders = new List<OrderRecord>();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            orders.Add(new OrderRecord
            {
                Id = reader.GetInt32(0),
                ItemsText = reader.IsDBNull(1) ? "" : reader.GetString(1),
                OrderDate = ParseDate(reader.GetString(2)),
                DeliveryDate = ParseDate(reader.GetString(3)),
                PickupPointId = reader.GetInt32(4),
                PickupPointAddress = reader.GetString(5),
                CustomerName = reader.GetString(6),
                ReceiveCode = reader.GetInt32(7),
                StatusId = reader.GetInt32(8),
                StatusName = reader.GetString(9)
            });
        }

        return orders;
    }

    private static DateTime ParseDate(string value)
    {
        return DateTime.TryParse(value, out var date) ? date : DateTime.Today;
    }

    private static void AddProductParameters(SqliteCommand command, ProductRecord product)
    {
        command.Parameters.AddWithValue("$article", product.Article);
        command.Parameters.AddWithValue("$name", product.Name);
        command.Parameters.AddWithValue("$unit_id", product.UnitId);
        command.Parameters.AddWithValue("$price", product.Price);
        command.Parameters.AddWithValue("$supplier_id", product.SupplierId);
        command.Parameters.AddWithValue("$manufacturer_id", product.ManufacturerId);
        command.Parameters.AddWithValue("$category_id", product.CategoryId);
        command.Parameters.AddWithValue("$discount", product.Discount);
        command.Parameters.AddWithValue("$stock_quantity", product.StockQuantity);
        command.Parameters.AddWithValue("$description", product.Description);
        command.Parameters.AddWithValue("$image_path", product.ImagePath);
    }
}
