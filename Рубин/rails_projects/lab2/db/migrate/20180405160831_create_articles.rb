class CreateArticles < ActiveRecord::Migration[5.1]
  def change
    create_table :articles do |t|
      t.integer :branches_id
      t.integer :author_id
      t.string :title
      t.string :text

      t.timestamps
    end
  end
end
