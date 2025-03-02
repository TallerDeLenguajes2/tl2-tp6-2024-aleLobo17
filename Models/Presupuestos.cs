using System;
using Microsoft.AspNetCore.Routing.Constraints;
using presupuestosDetalle;
using clientes;

namespace presupuestos
{

    public class Presupuestos
    {
        private int idPresupuesto;
        private Clientes clientes;
        private List<PresupuestoDetalle> detalle;
        private string fechaCreacion;

        public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
        
        public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }
        public string FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
        public Clientes Clientes { get => clientes; set => clientes = value; }

        public Presupuestos() { }
        public Presupuestos(int idPresupuesto, Clientes cliente, string fechaCreacion, List<PresupuestoDetalle> detalle)
    {
        this.idPresupuesto = idPresupuesto;
        this.clientes = cliente;
        this.fechaCreacion = fechaCreacion;
        this.detalle = detalle;
    }
    public Presupuestos(int idPresupuesto, Clientes cliente, string fechaCreacion)
    {
        this.idPresupuesto = idPresupuesto;
        this.clientes = cliente;
        this.fechaCreacion = fechaCreacion;
        detalle = new List<PresupuestoDetalle>();
    }

        public float MontoPresupuesto()
        {
            float acumuladorMonto = 0;
            foreach (var item in Detalle)
            {

                if (Detalle != null)
                {
                    acumuladorMonto =+ item.Producto.Precio;
                }

            }
            return acumuladorMonto;
        }

        public double MontoPresupuestoConIva()
        {
            double acumuladorMontoConIva = 0;
            foreach (var item in Detalle)
            {

                if (item != null)
                {
                    acumuladorMontoConIva = item.Producto.Precio + (item.Producto.Precio * 0.21) ;
                }

            }
            return acumuladorMontoConIva;
        }

        public int CantidadProductos()
        {
            int acumuladorCantidadProductos = 0;
            foreach (var item in Detalle){
                if (item != null){
                    acumuladorCantidadProductos++;
                }
            }
            return acumuladorCantidadProductos;
        }
    }

}