interface FormErrorProps {
  children?: React.ReactNode;
}

export function FormError({ children }: FormErrorProps) {
  if (!children) return null;

  return (
    <span className="text-red-500 text-xs">
      {children}
    </span>
  );
}