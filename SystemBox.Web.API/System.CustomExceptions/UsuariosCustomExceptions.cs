namespace System.CustomExceptions
{
    public class UsuariosCustomExceptions
    {
        public class UsuarioJaCadastradoException : Exception
        {
            public UsuarioJaCadastradoException(string message) 
                : base(message) { }

            public UsuarioJaCadastradoException(string message, Exception inner) 
                : base(message, inner) { }
        }

        public class UsuarioNaoExiste : Exception
        {
            public UsuarioNaoExiste(string message)
                : base(message) { }

            public UsuarioNaoExiste(string message, Exception inner)
                : base (message, inner) { }
        }
    }
}
