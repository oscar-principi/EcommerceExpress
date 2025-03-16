-- Crear la base de datos UsuariosEcommerce
CREATE DATABASE EcommerceUsuarios
ON
PRIMARY (
    NAME = 'Usuarios_data',
    FILENAME = 'D:\Osky\Proyectos\Ecommerce\Bases de datos\Usuarios_data.mdf',
    SIZE = 10MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 5MB
)
LOG ON (
    NAME = 'Usuarios_log',
    FILENAME = 'D:\Osky\Proyectos\Ecommerce\Bases de datos\Usuarios_log.ldf',
    SIZE = 5MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 1MB
);




-- Crear la base de datos CarritoEcommerce
CREATE DATABASE EcommerceCarrito
ON
PRIMARY (
    NAME = 'Carrito_data',
    FILENAME = 'D:\Osky\Proyectos\Ecommerce\Bases de datos\Carrito_data.mdf',
    SIZE = 10MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 5MB
)
LOG ON (
    NAME = 'Carrito_log',
    FILENAME = 'D:\Osky\Proyectos\Ecommerce\Bases de datos\Carrito_log.ldf',
    SIZE = 5MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 1MB
);





-- Crear la base de datos CatalogoEcommerce
CREATE DATABASE EcommerceCatalogo
ON
PRIMARY (
    NAME = 'Catalogo_data',
    FILENAME = 'D:\Osky\Proyectos\Ecommerce\Bases de datos\Catalogo_data.mdf',
    SIZE = 10MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 5MB
)
LOG ON (
    NAME = 'Catalogo_log',
    FILENAME = 'D:\Osky\Proyectos\Ecommerce\Bases de datos\Catalogo_log.ldf',
    SIZE = 5MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 1MB
);





-- Crear la base de datos PedidosEcommerce
CREATE DATABASE EcommercePedidos
ON
PRIMARY (
    NAME = 'Pedidos_data',
    FILENAME = 'D:\Osky\Proyectos\Ecommerce\Bases de datos\Pedidos_data.mdf',
    SIZE = 10MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 5MB
)
LOG ON (
    NAME = 'Pedidos_log',
    FILENAME = 'D:\Osky\Proyectos\Ecommerce\Bases de datos\Pedidos_log.ldf',
    SIZE = 5MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 1MB
);