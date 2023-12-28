import { Vacacion } from "./vacacion";

export interface EmpleadoDetalle {
  idEmpleado: number;
  nombreCompleto: string;
  fechaContrato:string;
  diasVacaciones: number;
  diasTomados: number;
  vacaciones: Vacacion[];
}
