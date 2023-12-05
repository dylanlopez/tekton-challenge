namespace Tekton.Domain.Dtos.Responses
{
	/// <summary>
	/// La clase Response<T> es una estructura genérica utilizada para encapsular respuestas estándar en operaciones, especialmente útil en el contexto de APIs y servicios donde se requiere una estructura uniforme para todas las respuestas.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Response<T>
	{
		/// <summary>
		/// Asigna un estado, mensaje y valor exitosos a la respuesta. Utilizado cuando una operación se completa exitosamente.
		/// </summary>
		/// <param name="value">El valor a ser asignado a la propiedad Value. Puede ser de cualquier tipo, dependiendo del contexto en el que se utilice la clase.</param>
		public void ResultOk(T value)
		{
			State = 200;
			Message = "Ok";
			Value = value;
		}

		/// <summary>
		/// Asigna un estado y mensaje de error a la respuesta. Utilizado cuando una operación falla o se encuentra con un error.
		/// </summary>
		/// <param name="error">El mensaje de error que se anexa al mensaje de la respuesta.</param>
		public void ResultError(string error)
		{
			State = 400;
			Message = "Error: " + error;
		}

		/// <summary>
		/// Un código numérico que representa el estado de la respuesta. Por ejemplo, 200 para éxito y 400 para error.
		/// </summary>
		public int State { get; set; }

		/// <summary>
		/// Un mensaje que describe el resultado de la operación. Puede ser un mensaje de éxito o un mensaje de error.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// El contenido específico de la respuesta. Puede ser de cualquier tipo, según lo definido por el tipo genérico T.
		/// </summary>
		public T Value { set; get; }
	}
}
