export function FormLabel({ children }: { children: React.ReactNode }) {
  return (
    <label className="text-sm font-medium">
      {children}
    </label>
  );
}