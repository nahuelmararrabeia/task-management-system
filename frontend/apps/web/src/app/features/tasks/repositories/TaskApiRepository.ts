import { TaskRepository } from "@/app/features/tasks/repositories/interfaces/TaskRepository";
import { Task } from "@/app/features/tasks/entities/Task";
import { apiClient } from "@/app/shared/http/clients";
import { User } from "@/app/features/users/entities/User";

export class TaskApiRepository implements TaskRepository {

  async getAll(): Promise<Task[]> {
    const res = await apiClient.get("/tasks");
    const data = await res.json();

    return data.items.map(this.mapToDomain);
  }

  async getById(id: string): Promise<Task | null> {
    const res = await apiClient.get(`/tasks/${id}`);

    if (res.status === 404) return null;

    const data = await res.json();
    return this.mapToDomain(data);
  }

  async create(task: Task): Promise<any> {
    const res = await apiClient.post("/tasks", this.mapToDto(task));
    return await res.json();
  }

  async update(task: Task): Promise<void> {
    await apiClient.put(`/tasks/${task.id}`, this.mapToDto(task));  
  }

  async updateStatus(id: string, status: string): Promise<void> {
    await apiClient.patch(`/tasks/${id}/status`, { status });
  }


  async delete(id: string): Promise<void> {
    await apiClient.delete(`/tasks/${id}`);
  }

  private mapToDomain(data: any): Task {
    return new Task({
      id: data.id,
      title: data.title,
      description: data.description,
      status: data.status,
      createdAt: new Date(data.createdAt),
      assignedUser: data.assignedUser 
        ? new User({ id: data.assignedUser.id, name: data.assignedUser.name }) 
        : undefined,
    });
  }

  private mapToDto(task: Task) {
    return {
      id: task.id,
      title: task.title,
      description: task.description,
      status: task.status,
      createdAt: task.createdAt,
    };
  }
}