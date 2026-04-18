import { NavigateToTasksBoardButton } from "./shared/components/NavigateToTasksBoardButton";

export default function HomePage() {
  return (
    <main className="min-h-screen bg-gray-50 flex items-center justify-center">
      <div className="bg-white p-8 rounded-xl border border-gray-200 shadow-sm">
        <h1 className="text-2xl font-semibold text-gray-900 mb-4">
          Task Management
        </h1>

        <p className="text-gray-600 mb-6">
          Bienvenido a tu sistema de gestión de tareas
        </p>

        <NavigateToTasksBoardButton />
      </div>
    </main>
  );
}