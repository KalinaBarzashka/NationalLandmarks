package com.infm308.explorebulgaria;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.infm308.explorebulgaria.models.Landmark;

import java.util.ArrayList;

public class LandmarkAdapter extends RecyclerView.Adapter<LandmarkAdapter.LandmarkHolder> {

    private final Context context;
    private ArrayList<Landmark> landmarks;

    public LandmarkAdapter(Context context, ArrayList<Landmark> landmarks) {
        this.context = context;
        this.landmarks = landmarks;
    }

    @NonNull
    @Override
    public LandmarkHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(context).inflate(R.layout.landmark_card_layout_item, parent, false); //landmark_layout_item
        return new LandmarkHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull LandmarkHolder holder, int position) {
        Landmark landmark = landmarks.get(position);
        holder.SetDetails(landmark);

        Button btn = holder.itemView.findViewById(R.id.readMoreBtn);
        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(context, LandmarkDetailsActivity.class);
                intent.putExtra("landmarkId", landmark.getId());
                context.startActivity(intent);
            }
        });
    }

    @Override
    public int getItemCount() {
        return landmarks.size();
    }

    public void setNewList(ArrayList<Landmark> landms) {
        this.landmarks = landms;
        notifyDataSetChanged();
    }

    static class LandmarkHolder extends RecyclerView.ViewHolder {

        private final TextView name;
        private final TextView description;
        private final TextView placeName;

        public LandmarkHolder(@NonNull View itemView) {
            super(itemView);

            name = itemView.findViewById(R.id.name_card);
            description = itemView.findViewById(R.id.description_card);
            placeName = itemView.findViewById(R.id.placeName_card);
        }
        void SetDetails(Landmark landmark){
            name.setText(landmark.getName());
            description.setText(landmark.getDescription());
            placeName.setText(landmark.getPlaceName());
        }
    }
}
