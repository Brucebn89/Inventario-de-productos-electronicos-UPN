using G11_SistemaInventario.Models;

namespace G5_SistemaInventario
{
    public partial class G5_FormularioPrincipal : Form
    {
        // Se asume un nombre de equipo G5. Adapta según tu "Número de Equipo".
        private List<G11_Product> G5_ListaProductos;
        private object dgvProductos;

        public G5_FormularioPrincipal()
        {
            InitializeComponent();
            G5_ListaProductos = new List<G11_Product>();
            G5_ConfigurarDataGridView(); // Configura las columnas del DataGridView
            G5_CargarCategorias(); // Carga algunas categorías de ejemplo
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        // Método para configurar las columnas del DataGridView
        private void G5_ConfigurarDataGridView()
        {
            // Limpia cualquier columna preexistente si se configuró desde el diseñador
            dgvProductos.Columns.Clear();

            // Añade las columnas manualmente
            dgvProductos.Columns.Add("G5_NombreCol", "Nombre");
            dgvProductos.Columns.Add("G5_CategoriaCol", "Categoría");
            dgvProductos.Columns.Add("G5_PrecioCol", "Precio");
            dgvProductos.Columns.Add("G5_CantidadCol", "Cantidad");
            dgvProductos.Columns.Add("G5_CodigoCol", "Código");

            // Opcional: Ajustar el ancho de las columnas
            object value = dgvProductos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        // Método para cargar categorías de ejemplo en el ComboBox
        private void G5_CargarCategorias()
        {
            // Asume que 'cmbCategoria' es el nombre de tu ComboBox
            if (cmbCategoria != null)
            {
                // Limpia los elementos existentes
                cmbCategoria.Items.Clear();

                // Añade algunas categorías de ejemplo
                cmbCategoria.Items.Add(new G5_Category(1, "Electrónica"));
                cmbCategoria.Items.Add(new G5_Category(2, "Electrodomésticos"));
                cmbCategoria.Items.Add(new G5_Category(3, "Informática"));
                cmbCategoria.Items.Add(new G5_Category(4, "Telefonia"));

                // Si quieres que una categoría esté seleccionada por defecto
                if (cmbCategoria.Items.Count > 0)
                {
                    cmbCategoria.SelectedIndex = 0;
                }
            }
        }


        // Evento Click del botón Registrar
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validar campos no vacíos (datos incompletos)
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    cmbCategoria.SelectedItem == null || // Verifica que se haya seleccionado una categoría
                    string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                    string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                    string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Sale del método si hay campos vacíos
                }

                // 2. Parsear y validar tipos de datos (Manejo de excepciones)
                // Usamos TryParse para evitar errores si el usuario ingresa texto no numérico
                decimal precio;
                if (!decimal.TryParse(txtPrecio.Text, out precio))
                {
                    MessageBox.Show("El precio debe ser un valor numérico válido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrecio.Focus(); // Pone el foco en el campo erróneo
                    return;
                }

                int cantidad;
                if (!int.TryParse(txtCantidad.Text, out cantidad))
                {
                    MessageBox.Show("La cantidad debe ser un número entero válido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCantidad.Focus();
                    return;
                }

                if (cantidad < 0)
                {
                    MessageBox.Show("La cantidad no puede ser negativa.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCantidad.Focus();
                    return;
                }
                if (precio < 0)
                {
                    MessageBox.Show("El precio no puede ser negativo.", "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrecio.Focus();
                    return;
                }


                string nombre = txtNombre.Text.Trim();
                G5_Category categoriaSeleccionada = (G5_Category)cmbCategoria.SelectedItem;
                string codigo = txtCodigo.Text.Trim();

                // 3. Validar si el producto ya existe (datos duplicados)
                // Se considera duplicado si el código ya existe
                if (G5_ListaProductos.Any(p => p.G5_Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show($"Ya existe un producto con el código '{codigo}'.", "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodigo.Focus();
                    return;
                }


                // 4. Crear una nueva instancia de Producto
                G5_Product nuevoProducto = new G5_Product(nombre, categoriaSeleccionada, precio, cantidad, codigo);

                // 5. Agregar el nuevo producto a la lista
                G5_ListaProductos.Add(nuevoProducto);

                // 6. Actualizar el DataGridView para mostrar el nuevo producto
                G5_ActualizarDataGridView();

                // 7. Limpiar los campos para un nuevo registro
                G5_LimpiarCampos();

                MessageBox.Show("Producto registrado exitosamente.", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción inesperada
                MessageBox.Show($"Ocurrió un error inesperado al registrar el producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para actualizar el DataGridView
        private void G5_ActualizarDataGridView()
        {
            dgvProductos.Rows.Clear(); // Limpia todas las filas existentes

            foreach (var producto in G5_ListaProductos)
            {
                // Agrega una nueva fila al DataGridView con los datos del producto
                dgvProductos.Rows.Add(
                    producto.G5_Nombre,
                    producto.G5_Categoria.G5_Name, // Accedemos al nombre de la categoría
                    producto.G5_Precio,
                    producto.G5_Cantidad,
                    producto.G5_Codigo
                );
            }
            // Opcional: Ajustar el ancho de las columnas después de agregar datos
            dgvProductos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        // Método para limpiar los campos de entrada
        private void G5_LimpiarCampos()
        {
            txtNombre.Clear();
            // Mantener la categoría seleccionada o resetearla a la primera
            if (cmbCategoria.Items.Count > 0)
            {
                cmbCategoria.SelectedIndex = 0;
            }
            txtPrecio.Clear();
            txtCantidad.Clear();
            txtCodigo.Clear();
            txtNombre.Focus(); // Pone el foco en el primer campo para facilitar el siguiente registro
        }

        // Puedes añadir eventos para los botones del menú superior (Inicio, Stock, Salir) más adelante
        private void G5_FormularioPrincipal_Load(object sender, EventArgs e)
        {
            // Código a ejecutar cuando el formulario carga
        }
    }
}