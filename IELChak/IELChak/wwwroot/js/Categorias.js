
class Categorias {
    constructor(nombre, descripcion, estado, action) {
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.estado = estado;
        this.action = action
    }

    agregarCategoria() {
        if (this.nombre == "") {
            document.getElementById("Nombre").focus();
        } else {
            if (this.descripcion == "") {
                document.getElementById("Descripcion").focus();
            }
            else {
                if (this.estado == "0") {
                    document.getElementById("mensaje").innerHTML = "Seleccione un estado";
                }
                else {
                    alert(this.nombre);
                    var nombre = this.nombre;
                    var descripcion = this.descripcion;
                    var estado = this.estado;
                    var action = this.action;
                    var mensaje = '';
                    $.ajax({
                        type: "POST",
                        url: action,
                        data: {
                            nombre, descripcion, estado
                        },
                        success: (response) => {

                        }
                    });
                }
            }
        }
    }
}

var agregarCategoria = () => {
    var nombre = document.getElementById("Nombre").value;
    var descripcion = document.getElementById("Descripcion").value;
    var estados = document.getElementById('Estado');
    var estado = estados.options[estados.selectedIndex].value;
    var action = 'Categorias/guardarCategoria';
    var categoria = new Categorias(nombre, descripcion, estado, action);
    categoria.agregarCategoria();
}
