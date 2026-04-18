import type { Meta, StoryObj } from "@storybook/react";
import { KanbanBoard } from "./KanbanBoard";
import { KanbanColumnData } from "./types";
import { useState } from "react";

const meta: Meta<typeof KanbanBoard> = {
  title: "Components/Kanban/KanbanBoard",
  component: KanbanBoard,
};

export default meta;

type Story = StoryObj<typeof KanbanBoard>;

const sampleColumns: KanbanColumnData[] = [
  {
    id: "todo",
    title: "To Do",
    cards: [
      { id: "1", content: "Task 1" },
      { id: "2", content: "Task 2" },
    ],
  },
  {
    id: "doing",
    title: "Doing",
    cards: [
      { id: "3", content: "Task 3" },
    ],
  },
  {
    id: "done",
    title: "Done",
    cards: [
      { id: "4", content: "Task 4" },
    ],
  },
];

export const Default: Story = {
  args: {
    columns: sampleColumns,
  },
};

export const EmptyColumns: Story = {
  args: {
    columns: [
      { id: "todo", title: "To Do", cards: [] },
      { id: "doing", title: "Doing", cards: [] },
    ],
  },
};

export const ManyColumns: Story = {
  args: {
    columns: Array.from({ length: 10 }).map((_, i) => ({
      id: `col-${i}`,
      title: `Column ${i + 1}`,
      cards: [
        { id: `card-${i}-1`, content: "Example task" },
      ],
    })),
  },
};

export const CustomContent: Story = {
  args: {
    columns: [
      {
        id: "todo",
        title: <span>🔥 To Do</span>,
        cards: [
          {
            id: "1",
            content: (
              <div>
                <strong>Custom Card</strong>
                <p>Description here</p>
              </div>
            ),
          },
        ],
      },
    ],
  },
};

export const Interactive: Story = {
  render: () => {
    const [columns, setColumns] = useState(sampleColumns);

    const handleCardMove = ({
      cardId,
      fromColumnId,
      toColumnId,
      toIndex,
    }: {
      cardId: string;
      fromColumnId: string;
      toColumnId: string;
      toIndex: number;
    }) => {
      setColumns((prev) => {
        const newColumns = structuredClone(prev);

        const fromCol = newColumns.find(c => c.id === fromColumnId);
        const toCol = newColumns.find(c => c.id === toColumnId);

        if (!fromCol || !toCol) return prev;

        const card = fromCol.cards.find(c => c.id === cardId);
        if (!card) return prev;

        // remover de origen
        fromCol.cards = fromCol.cards.filter(c => c.id !== cardId);

        // agregar a destino
        toCol.cards.splice(toIndex, 0, card);

        return newColumns;
      });
    };

    return (
      <KanbanBoard
        columns={columns}
        onCardMove={handleCardMove}
      />
    );
  },
};